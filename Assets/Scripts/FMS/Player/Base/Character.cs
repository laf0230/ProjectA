using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable, IMoveable, ITriggerCheckable, ITargetingable
{
    [field: SerializeField] public float MaxHealth { get; set; }
    [field: SerializeField] public float ChaseSpeed { get; set; } = 1.75f;
    public float CurrentHealth { get; set; }
    public Rigidbody Rigidbody { get; set; }
    public bool IsFacingRight { get; set; } = true;
    public bool IsAggroed { get; set; }
    public bool IsWithinstrikingDistance { get; set; }
    public SpriteRenderer Renderer { get; set; }
    public Animator Animator { get; set; }
    public bool IsPassThrough { get; set; }
    [field: SerializeField] public int ThreatLevel { get; set; }
    public GameObject Target { get; set; }
    // 동시에 여러 줄을 작성할 때 쿼리가 작동하면 취소됨 -> 수정할 것.

    #region StateMachine Variables

    public StateMachine StateMachine { get; set; }
    public CharacterIdleState IdleState { get; set; }
    public CharacterChaseState ChaseState { get; set; }
    public CharacterAttackState AttackState { get; set; }

    #endregion

    #region Idle Variables

    public float RandomMovementRange = 5f;

    #endregion

    private void Awake()
    {
        StateMachine = new StateMachine();

        IdleState = new CharacterIdleState(this, StateMachine);
        ChaseState = new CharacterChaseState(this, StateMachine);
        AttackState = new CharacterAttackState(this, StateMachine);
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;

        Rigidbody = GetComponent<Rigidbody>();

        Renderer = GetComponent<SpriteRenderer>();

        Animator = GetComponent<Animator>();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentPlayerState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentPlayerState.PhysicsUpdate();
    }

    #region Health / Die Functions

    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        
        if (CurrentHealth < 0)
        {
            Die();
        } else
        {
            AnimationTriggerEvent(AnimationTriggerType.Hurt);
        }
    }

    public void Die()
    {
        
    }



    #endregion

    #region Movement Functions

    public void MoveEnemy(Vector3 velopcity)
    {
        Rigidbody.velocity = new Vector3(velopcity.x, 0f, velopcity.z);

        CheckForLeftOrRightFacing(velopcity);
    }

    public void CheckForLeftOrRightFacing(Vector3 velopcity)
    {
        if(IsFacingRight && velopcity.x > 0f)
        {
            Renderer.flipX = true;
        }
        else if(IsFacingRight && velopcity.x < 0f)
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

    #region Occupation

    public enum Distance_Basis    {
        
    }

    #endregion

    #region Triggers

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
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
        }
    }

    public enum AnimationTriggerType
    {
        Hurt,
        Attack
    }

    #endregion

    #region Targetting

    
    public void SetTarget(GameObject target)
    {
        this.Target = target;
    }

    #endregion
}
