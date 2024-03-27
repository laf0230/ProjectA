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

    public override void AnimationTriggerEvent(Character.TriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        _targetTransform = character.Target.transform;

        _moveSpeed = character.ChaseSpeed;

        _targetCharacter = character.Target.GetComponent<Character>();
   }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        Debug.Log(character.IsPassThrough);

        if (character.ThreatLevel <_targetCharacter.ThreatLevel)
        {
            character.IsPassThrough = true;
        }
        else
        {
            character.IsPassThrough = false;
        }
 

        if (character.IsPassThrough)
        {
            // PassThrough
            character.MoveEnemy(-(_targetTransform.position - character.transform.position).normalized);
        }
        else
        {
            // Chasing Code
            character.MoveEnemy((_targetTransform.position - character.transform.position).normalized);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
