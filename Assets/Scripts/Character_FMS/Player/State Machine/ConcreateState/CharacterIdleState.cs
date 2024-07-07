using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdleState : State
{
    private Vector3 _targetPos;
    private Vector3 _direction;
    private Character _self;
    private  int _targetThreatLevel;

    public CharacterIdleState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        _self = character;
    }

    public override void AnimationTriggerEvent(Character.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        
        base.EnterState();

        /*
        _targetPos = GetRandomPointInCircle();
        character.IsMoveable = true;
        */
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (character.IsAggroed)
        {
            _targetThreatLevel = character.Target.GetComponent<Character>().ThreatLevel;
            if (character.ThreatLevel >= _targetThreatLevel)
            {
                // ÃßÀûÇÒ ¶§
                character.StateMachine.ChangeState(character.ChaseState);
            }
            else if (character.ThreatLevel < _targetThreatLevel)
            {
                // µµ¸ÁÄ¥ ¶§
                character.StateMachine.ChangeState(character.EscapeState);
            }
        }
        /*
        _direction = (_targetPos - character.transform.position).normalized;
       character.MoveTo(_direction);
        */
        /*       
               if ((character.transform.position - _targetPos).sqrMagnitude < 0.01f)
               {
                   _targetPos = GetRandomPointInCircle();
               }
        */
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public Vector3 GetRandomPointInCircle()
    {
        Vector3 randomPosition = character.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * character.RandomMovementRange;
        return new Vector3(randomPosition.x, 0f, randomPosition.z);
    }
}
