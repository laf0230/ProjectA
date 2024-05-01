using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        if (character.ThreatLevel <_targetCharacter.ThreatLevel)
        {
            character.IsPassThrough = true;
        }
        else
        {
            character.IsPassThrough = false;
        }
 
        if (!_targetCharacter.gameObject.activeSelf)
        {
            // 적이 사망했을 때
            Debug.Log("적이 사라졌습니다");
            character.StateMachine.ChangeState(character.IdleState);
        }

        if (character.IsPassThrough)
        {
            // 도망 칠 때
            // PassThrough
            character.StateMachine.ChangeState(character.EscapeState);
        }
        else
        {
            // 추적할 때
            // Chasing Code
            character.MoveTo(((_targetTransform.position - character.transform.position) * _moveSpeed).normalized, _moveSpeed);
        }

        if (character.IsWithinstrikingDistance && character.Attack.isAttackable || character.Skill.isAttackable || character.SpecialSkill.isAttackable)
        {
            // 공격할 때
            character.StateMachine.ChangeState(character.AttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
} 