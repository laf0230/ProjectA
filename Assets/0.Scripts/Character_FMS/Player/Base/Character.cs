using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Character : MonoBehaviour, IDamageable, IMoveable, ITriggerCheckable
{
    [field: SerializeField] public float MaxHealth { get; set; }
    [field: SerializeField] public float ChaseSpeed { get; set; } = 1.75f;
    [field: SerializeField] public float CurrentHealth { get; set; }
    public Rigidbody Rigidbody { get; set; }
    public bool IsFacingRight { get; set; } = true;
    public bool IsAggroed { get; set; }
    public bool IsWithinstrikingDistance { get; set; }
    public SpriteRenderer Renderer { get; set; }
    public Animator Animator { get; set; }
    [field: SerializeField] public bool IsPassThrough { get; set; }
    [field: SerializeField] public int ThreatLevel { get; set; }
    [field: SerializeField] public GameObject Target { get; set; }
    [field: SerializeField] public GameObject Bullet { get; set; }
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
    [field: SerializeField] public List<SkillDataSO> skillData { get; set; }
    public bool IsAttackable { get; set; } = true;
    public bool IsBuffable { get; set; } = false;
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
        CurrentHealth = MaxHealth;

        Rigidbody = GetComponent<Rigidbody>();

        Renderer = GetComponentInChildren<SpriteRenderer>();

        Animator = GetComponentInChildren<Animator>();

        StateMachine.Initialize(IdleState);

        foreach(var data in skillData)
        {
            // 등록된 스킬 데이터의 수만큼 스킬 추가 및 초기화
            var skill = gameObject.AddComponent<Combat>();
            combats.Add(skill);
            skill.Initialize( new SkillInfo(
                    data.Name, 
                    data.Type, 
                    data.rangeType, 
                    data.targetType,
                    data.CoolTime, 
                    new BulletInfo(data.Damage, data.Speed, transform)
                    , data.Ability
                    ));

            skill.bulletSettings.bulletPrefab = Bullet;
        }
    }

    public void Update()
    {
        StateMachine.CurrentPlayerState.FrameUpdate();

        if (GameObject.FindGameObjectsWithTag("Character").Length <= 1)
        {
            GameManager.Instance.GameEnd();
        }

        // 사용할 수 있는 스킬이 있을 경우에 true 값 설정
        IsAttackable = combats.Any(combat => combat.IsUseable());
    }

    public void FixedUpdate()
    {
        StateMachine.CurrentPlayerState.PhysicsUpdate();    
    }

    #region Health / Die Functions

    public virtual void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        Debug.Log("Damage: " + damageAmount);
        Debug.Log("CurrentHealth" + CurrentHealth);

        if (CurrentHealth < 0)
        {
            Debug.Log("I'm Died");
            Die();
        }
        else
        {
            AnimationTriggerEvent(AnimationTriggerType.Hurt);
            Stun();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    #endregion

    #region Movement Functions

    public void SetMoveAble(bool isActive)
    {
        NavMeshAgent agent =  GetComponent<NavMeshAgent>();

        agent.enabled = isActive;
    }

    public void MoveTo(Vector3 velocity)
    {
            Rigidbody.velocity = new Vector3(velocity.x, 0f, velocity.z);
        
            CheckForLeftOrRightFacing(velocity);
    }

    public void MoveTo(Vector3 velocity, float speed)
    {
            Rigidbody.velocity = new Vector3(velocity.x, 0f, velocity.z) * speed;

            CheckForLeftOrRightFacing(velocity);
    }

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

    // Basic Stun
    public IEnumerator Stun()
    {
        SetMoveAble(true);
        yield return new WaitForSeconds(StunTime);
        SetMoveAble(false);
    }

    // Skill Stun
    public IEnumerator Stun(float stunTime)
    {
        SetMoveAble(true);
        yield return new WaitForSeconds(stunTime);
        SetMoveAble(false);
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
                Animator.SetFloat("Run", 0);
                Animator.SetTrigger("Attack");
                break;
            case AnimationTriggerType.Hurt:
                Animator.SetFloat("Run", 0);
                Animator.SetTrigger("Hurt");
                break;
            case AnimationTriggerType.Skill:
                Animator.SetFloat("Run", 0);
                Animator.SetTrigger("Skill");
                break;
            case AnimationTriggerType.SpecialSkill:
                Animator.SetFloat("Run", 0);
                Animator.SetTrigger("SpecialSkill");
                break;
            case AnimationTriggerType.Run:
                Animator.SetFloat("Run", 5);
                break;
            default:
                Debug.LogError($"Unhandled triggerType: {triggerType}");
                break;

        }
                Debug.Log("Current Animation State: " + triggerType.ToString());
    }

    public enum AnimationTriggerType
    {
        Run,
        Hurt,
        Attack,
        Skill,
        SpecialSkill
    }

    #endregion

}
