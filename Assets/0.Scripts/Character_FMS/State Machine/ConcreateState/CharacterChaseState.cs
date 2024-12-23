using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChaseState : State
{
    private Transform _targetTransform;
    private Character _targetCharacter;
    private float _moveSpeed = 1.75f;

    public CharacterChaseState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void AnimationTriggerEvent(Character.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        AnimationTriggerEvent(Character.AnimationTriggerType.Run);

        _targetTransform = character.Targets[0].transform;

        _moveSpeed = character.Info.Status.ChaseSpeed;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (!_targetTransform.gameObject.activeSelf || !character.IsAggroed)
        {
            // 적이 사망했을 때
            character.StateMachine.ChangeState(character.IdleState);
        }

        if (character.IsWithinstrikingDistance && character.IsAttackable)
        {
            character.StateMachine.ChangeState(character.AttackState);
        }

        // Vector3 direction = (_targetTransform.position - character.transform.position).normalized;

        character.MoveTo(_targetTransform.position, _moveSpeed);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}