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

        Debug.Log("µµ¸ÁÃÄ!");
        _targetTransform = character.Target.transform; 
        _moveSpeed = character.ChaseSpeed;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        character.MoveTo((-(_targetTransform.position - character.transform.position) * _moveSpeed).normalized, _moveSpeed);

        if (!character.IsAggroed)
        {
            character.StateMachine.ChangeState(character.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
