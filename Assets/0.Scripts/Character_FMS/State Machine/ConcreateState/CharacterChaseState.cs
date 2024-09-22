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

        AnimationTriggerEvent(Character.AnimationTriggerType.Run);

        _targetTransform = character.Target.transform;

        _moveSpeed = character.Status.ChaseSpeed;

        _targetCharacter = character.Target.GetComponent<Character>();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (!_targetCharacter.gameObject.activeSelf || !character.IsAggroed)
        {
            // 적이 사망했을 때
            character.StateMachine.ChangeState(character.IdleState);
        }

        if (character.IsWithinstrikingDistance && character.IsAttackable)
        {
            Debug.Log("Enemy check, I can use skill");
            character.StateMachine.ChangeState(character.AttackState);
        }

        character.MoveTo(((_targetTransform.position - character.transform.position) * _moveSpeed).normalized, _moveSpeed);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}