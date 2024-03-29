using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackState : State
{
    private Transform _transform;
    private Character _target;
    private float _timerBetweenShoots = 2f;

    private float _exitTimer;
    private float _timeTillExit = 3f;
    private float _distanceToCountExit = 3f;

    private float _bulletSpeed = 10f;

    public CharacterAttackState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        _transform = GameObject.FindGameObjectWithTag("Character").transform;
    }

    public override void AnimationTriggerEvent(Character.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        _target = character.Target.GetComponent<Character>();

        AnimationTriggerEvent(Character.AnimationTriggerType.Attack);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    #region Animation

    public override void EnterAnim()
    {
        base.EnterAnim();
    }

    public void AttackTiming()
    {
        // _target.Damage();
    }

    public override void ExitAnim()
    {
        base.ExitAnim();
    }

    #endregion
}
