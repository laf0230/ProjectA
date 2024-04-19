using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEscapeState : State
{
    Transform _targetTransform;
    float _moveSpeed;
    float _attackCoolTime;
    public CharacterEscapeState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _targetTransform = character.Target.transform;
        _moveSpeed = character.ChaseSpeed;
        _attackCoolTime = character.AttackCoolTime;
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (!(character.StateMachine.CurrentPlayerState == character.AttackState))
        {
            character.MoveTo((-(_targetTransform.position - character.transform.position) * _moveSpeed).normalized, _moveSpeed);
        }

        if (!character.IsAggroed)
        {
            // �ν� ���� ���� ���� ���� ���
            character.StateMachine.ChangeState(character.IdleState);
        }
        else if (character.IsAggroed && _attackCoolTime <= 0)
        {
            // ����ġ�鼭 ����
            // �߰� ���� �ʿ� ��) �������ٰ� �����ϴ� term
            character.StateMachine.ChangeState(character.AttackState);
        }
        else if (character.IsAggroed && _attackCoolTime >= 0)
        {
            // �⺻ ���� ��Ÿ��
            _attackCoolTime -= Time.deltaTime;
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
