using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEscapeState : State
{
    Transform _targetTransform;
    float _moveSpeed;

    public CharacterEscapeState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        AnimationTriggerEvent(Character.AnimationTriggerType.Run);

        _targetTransform = character.Target.transform;
        _moveSpeed = character.Status.ChaseSpeed;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (!character.IsAggroed)
        {
            // 인식 범위 내에 적이 없을 경우
            character.StateMachine.ChangeState(character.IdleState);
        }

        character.MoveTo((-(_targetTransform.position - character.transform.position) * _moveSpeed).normalized, _moveSpeed);
    }

    public override void ExitState()
    {
        base.ExitState();
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
