using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Character : MonoBehaviour, IDamageable, IMoveable, ITriggerCheckable
{
    [field:SerializeField] public CharacterInfoSO Info { get; private set; }
    [field:SerializeField] public CharacterStatus Status { get; set; }
    [field: SerializeField] public float CurrentHealth { get; set; } = 0;
    public Rigidbody Rigidbody { get; set; }
    public NavMeshAgent Agent { get; set; }
    public bool IsFacingRight { get; set; } = true;
    public bool IsAggroed { get; set; }
    public bool IsWithinstrikingDistance { get; set; }
    public SpriteRenderer Renderer { get; set; }
    public Animator Animator { get; set; }
    [field: SerializeField] public bool IsPassThrough { get; set; }
    [field: SerializeField] public int ThreatLevel { get; set; }
    [field: SerializeField] public List<GameObject> Targets { get; set; }
    [field: SerializeField] private Slider slider { get; set; }

    public bool isDead { get; set; } = false;

    // 동시에 여러 줄을 작성할 때 쿼리가 작동하면 취소됨 -> 수정할 것.

    #region StateMachine Variables

    public StateMachine StateMachine { get; set; }
    public CharacterIdleState IdleState { get; set; }
    public CharacterChaseState ChaseState { get; set; }
    public CharacterAttackState AttackState { get; set; }
    public CharacterEscapeState EscapeState { get; set; }

    #endregion

    #region Skill Variables
    
    [field: SerializeField] public List<Combat> combats { get; set; }
    // [field: SerializeField] public List<SkillSO> skillData { get; set; }
    public bool IsAttackable { get; set; } = true;
    public bool IsBuffable { get; set; } = false;
    public bool IsMoveAble {  get; set; }
    #endregion

    #region Idle Variables

    public float RandomMovementRange = 5f;

    #endregion

    #region Battle Variables

    public float StunTime { get; set; } = 1f;
    public bool IsRestriction { get; set; }
    
    #endregion

    public void Awake()
    {
        StateMachine = new StateMachine();

        IdleState = new CharacterIdleState(this, StateMachine);
        ChaseState = new CharacterChaseState(this, StateMachine);
        AttackState = new CharacterAttackState(this, StateMachine);
        EscapeState = new CharacterEscapeState(this, StateMachine);
    }

    public virtual void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();

        Renderer = GetComponentInChildren<SpriteRenderer>();

        Animator = GetComponentInChildren<Animator>();

        StateMachine.Initialize(IdleState);

        Agent = GetComponent<NavMeshAgent>();

        Status = Info.Status;
        
        CurrentHealth += Status.MaxHealth;

        foreach(var data in Info.Skills)
        {
            Debug.Log(data.Type);
            // 등록된 스킬 데이터의 수만큼 스킬 추가 및 초기화
            var skill = gameObject.AddComponent<Combat>();
            skill.Initialize(new SkillProperties(
                transform,
                data.Type,
                data.ShapeType,
                data.TargetType,
                data.CoolTime,
                data.ProjectileType,
                data.Damage,
                data.Speed,
                data.Reach,
                data.Ability,
                data.IsUsedOwnPlace,
                data.Duration
                ));
            combats.Add(skill);
        }

        ThreatLevel = Info.Status.ThreatLevel;
    }

    private void OnEnable()
    {
        // 아이템 스테이터스 적용
        ApplyItemEffect();
    }

    public void Update()
    {
        StateMachine.CurrentPlayerState.FrameUpdate();

        if (CurrentHealth > 0)
        {
            slider.value = CurrentHealth / Info.Status.MaxHealth;
        } else
        {
            slider.value = 0;
        }

        Targets.RemoveAll(target => Targets != null && !target.activeSelf);

        // 사용할 수 있는 스킬이 있을 경우에 true 값 설정
        IsAttackable = combats.Any(combat => combat.IsUseable());
    }

    public void FixedUpdate()
    {
        StateMachine.CurrentPlayerState.PhysicsUpdate();    
    }

    public void ApplyItemEffect()
    {
        Debug.Log("아이템 효과 적용을 시작합니다.");
        foreach (var item in Info.investData.investedItems)
        {
            Debug.Log(item.Name + " 아이템의 스테이터스 적용을 시작합니다.");
            var abilities = AbilityManager.Instance.GetAbilities(item.Ability, this);
            foreach (var ability in abilities)
            {
                Debug.Log("캐릭터가 아이템으로 변화될 스테이터스의 ID는 " + ability.Info.ID);
                Debug.Log("캐릭터가 아이템으로 변화될 스테이터스는 " + ability.Info.EffectStatus + "입니다.");

                ability.use(transform);
            }
            Debug.Log(item.Name + " 아이템 효과가 정상적으로 적용되었습니다/!");
        }
    }

    #region Health / Die Functions

    public virtual void Damage(float damageAmount)
    {
        // 데미지 계산식
        // 투사체의 피해량 x (100 - 방어력) / 100 x 랜덤(80~110) / 100
        var totalDamage = damageAmount * (100 - Info.Status.ArmorValue) / 100 * Random.Range(80, 110) / 100;
        CurrentHealth -= totalDamage;

        if (CurrentHealth < 0)
        {
            isDead = true;
            UIManager_.Instance.rankingUI.SetDeathOrder(Info);
            Debug.Log("I'm Died");
            AnimationTriggerEvent(AnimationTriggerType.Dead);
        }
        else
        {
            // AnimationTriggerEvent(AnimationTriggerType.Hurt);
            StartCoroutine(Hurt());
        }
    }

    private IEnumerator Hurt()
    {
        Renderer.color = new Color(1f, 0.7f, 0.7f);
        yield return new WaitForSeconds(0.1f);
        Renderer.color = new Color(1f, 1f, 1f);
    }

    public void StartDieProcess()
    {
        StartCoroutine(Die());
    }

    public IEnumerator Die()
    {
        Debug.Log(Info.Profile.Name + " 캐릭터가 사망했습니다!");
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    #endregion

    #region Movement Functions

    public void SetMoveAble(bool isActive)
    {
        IsMoveAble = isActive;

        Agent.velocity = Vector3.zero;
        Agent.isStopped = !isActive;
    }

    public void MoveTo(Vector3 velocity)
    {
            // Rigidbody.velocity = new Vector3(velocity.x, 0f, velocity.z);
        Agent.Move(new Vector3(velocity.x, 0f, velocity.z));
        
        CheckForLeftOrRightFacing(velocity);
    }


    public void MoveTo(Vector3 targetPosition, float speed = 1f)
    {
        // Debug.Log("Speed Value: " + speed + ", Target: " + target);
        // Rigidbody.velocity = new Vector3(velocity.x, 0f, velocity.z) * speed;
        Agent.speed = speed;
        Agent.SetDestination(targetPosition);

        CheckForLeftOrRightFacing(targetPosition);
    }

    /*
    public void MoveTo(Vector3 velocity, float speed = 1f)
    {
        // Rigidbody.velocity = new Vector3(velocity.x, 0f, velocity.z) * speed;
        Agent.speed = speed;
        Agent.velocity = new Vector3(velocity.x, 0f, velocity.z);
        Agent.SetDestination(velocity);

        CheckForLeftOrRightFacing(velocity);
    }
    */

    public void CheckForLeftOrRightFacing(Vector3 velocity)
    {
        if (IsFacingRight && velocity.x > 0f)
        {
            Renderer.flipX = true;
        }
        else if (IsFacingRight && velocity.x < 0f)
        {
            Renderer.flipX = false;
        }
    }

    #endregion

    #region Distance Checks

    public void SetAggrostatus(bool isAggroed)
    {
        IsAggroed = isAggroed;
    }

    public void SetStrikingDistance(bool isWithinstrikingDistance)
    {
        IsWithinstrikingDistance = isWithinstrikingDistance;
    }

    #endregion

    #region Triggers

    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentPlayerState.AnimationTriggerEvent(triggerType);

        switch (triggerType)
        {
            case AnimationTriggerType.Attack:
                Animator.SetTrigger("Attack");
                break;
            case AnimationTriggerType.Hurt:
                Animator.SetTrigger("Hurt");
                break;
            case AnimationTriggerType.Skill:
                Animator.SetTrigger("Skill");
                break;
            case AnimationTriggerType.SpecialSkill:
                Animator.SetTrigger("SpecialSkill");
                break;
            case AnimationTriggerType.Run:
                Animator.Play("Walk");
                Animator.SetFloat("Run", 5);
                Debug.Log("달리는 중입니다!");
                break;
            case AnimationTriggerType.Dead:
                Animator.SetFloat("Run", 0);
                Animator.SetBool("IsDead", true);
                break;
            default:
                Debug.LogError($"Unhandled triggerType: {triggerType}");
                break;

        }
                // Debug.Log("Current Animation State: " + triggerType.ToString());
    }

    public enum AnimationTriggerType
    {
        Run,
        Hurt,
        Attack,
        Skill,
        SpecialSkill,
        Dead
    }

    #endregion

}
