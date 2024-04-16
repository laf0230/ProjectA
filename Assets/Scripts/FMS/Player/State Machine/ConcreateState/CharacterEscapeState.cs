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

        _targetTransform = character.Target.transform; 
        _moveSpeed = character.ChaseSpeed;
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        character.MoveTo((-(_targetTransform.position - character.transform.position) * _moveSpeed).normalized, _moveSpeed);

        if (!character.IsAggroed)
        {
            character.StateMachine.ChangeState(character.IdleState);
        } else if(character.IsAggroed)
        {
            // 도망치면서 공격
            // 추가 조건 필요 예) 도망가다가 공격하는 term
            character.StateMachine.ChangeState(character.AttackState);
        }

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
