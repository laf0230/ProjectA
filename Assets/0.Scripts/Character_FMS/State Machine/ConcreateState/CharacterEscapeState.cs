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

        _targetTransform = character.Targets[0].transform;
        _moveSpeed = character.Status.ChaseSpeed;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (!character.IsAggroed)
        {
            // �ν� ���� ���� ���� ���� ���
            character.StateMachine.ChangeState(character.IdleState);
        }
        Vector3 direction = -(_targetTransform.position - character.transform.position).normalized;

        character.MoveTo(_targetTransform, _moveSpeed);
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
