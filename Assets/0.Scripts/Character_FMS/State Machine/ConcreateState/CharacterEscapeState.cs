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

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (!_targetTransform.gameObject.activeSelf || !character.IsAggroed)
        {
            // 적이 사망했을 때 && 범위 내에 적이 사라졌을 때
            character.StateMachine.ChangeState(character.IdleState);
        }

        if (character.IsWithinstrikingDistance && character.IsAttackable)
        {
            // 공격 범위에 있을 때
            Debug.Log("Enemy check, I can use skill");
            character.StateMachine.ChangeState(character.AttackState);
        }

        // 역방향 계산
        Vector3 targetPosition = 2 * (character.transform.position - _targetTransform.position);

        // Vector3 direction = (_targetTransform.position - character.transform.position).normalized;
        // 역방향으로 도망
        character.MoveTo(targetPosition, _moveSpeed);
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
