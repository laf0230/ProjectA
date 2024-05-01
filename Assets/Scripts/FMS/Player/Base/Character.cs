using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable, IMoveable, ITriggerCheckable, ITargetingable, ISkillable
{
    [field: SerializeField] public float MaxHealth { get; set; }
    [field: SerializeField] public float ChaseSpeed { get; set; } = 1.75f;
    [field: SerializeField] public float CurrentHealth { get; set; }
    [field: SerializeField] public GameObject DamageTrigger { get; set; }
    public Rigidbody Rigidbody { get; set; }
    public bool IsFacingRight { get; set; } = true;
    public bool IsAggroed { get; set; }
    public bool IsWithinstrikingDistance { get; set; }
    public SpriteRenderer Renderer { get; set; }
    public Animator Animator { get; set; }
    [field: SerializeField] public bool IsPassThrough { get; set; }
    [field: SerializeField] public int ThreatLevel { get; set; }
    [field: SerializeField] public GameObject Target { get; set; }
    // 동시에 여러 줄을 작성할 때 쿼리가 작동하면 취소됨 -> 수정할 것.

    #region StateMachine Variables

    public StateMachine StateMachine { get; set; }
    public CharacterIdleState IdleState { get; set; }
    public CharacterChaseState ChaseState { get; set; }
    public CharacterAttackState AttackState { get; set; }
    public CharacterEscapeState EscapeState { get; set; }

    #endregion

    #region Skill Variables

    public Attack Attack { get; set; }
    public Skill Skill { get; set; }
    public SpecialSkill SpecialSkill { get; set; }
    [field: SerializeField] public SkillDataSO AttackDataSO { get; set; }
    [field: SerializeField] public SkillDataSO SkillDataSO {get; set; }
    [field: SerializeField] public SkillDataSO SpecialSkillDataSO {get; set; }
    public bool IsAttackable { get; set; } = true;

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

    public void Start()
    {
        CurrentHealth = MaxHealth;

        Rigidbody = GetComponent<Rigidbody>();

        Renderer = GetComponent<SpriteRenderer>();

        Animator = GetComponent<Animator>();

        StateMachine.Initialize(IdleState);

        // Attack = BattleManager.instance.GetAttack(Attack);
        // Skill = BattleManager.instance.GetAttack(Skill);
        // SpecialSkill = BattleManager.instance.GetAttack(SpecialSkill);


    }

    public void Update()
    {
        StateMachine.CurrentPlayerState.FrameUpdate();
    }

    public void FixedUpdate()
    {
        StateMachine.CurrentPlayerState.PhysicsUpdate();
    }

    #region

    public void StartCoolTime(float _coolTime)
    {
        // 스킬별 쿨타임 설정 
    }

    #endregion

    #region Health / Die Functions

    public virtual void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;

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
        yield return new WaitForSeconds(StunTime);
        StateMachine.ChangeState(IdleState);
        Debug.Log("I'm Stuned");
    }

    // Skill Stun
    public IEnumerator Stun(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        StateMachine.ChangeState(IdleState);
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

    #region Occupation

    public enum Distance_Basis
    {

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
                Animator.SetTrigger("Run");
                break;
            default:
                Debug.LogError($"Unhandled triggerType: {triggerType}");
                break;
        }
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

    #region Targetting

    public void SetTarget(GameObject target)
    {
        this.Target = target;
    }

    #endregion
}
