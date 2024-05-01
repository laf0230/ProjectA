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

        if (!character.IsAggroed)
        {
            // �ν� ���� ���� ���� ���� ���
            character.StateMachine.ChangeState(character.IdleState);
        }

        // ���� �߰� 2++024 04 29
        // Escape���� Attack�� ���� ���� ���� Chase�� �̵� �� ���� " �ٷ�"  Escape�� ����
        // Attack�� �ϴ� ���� -> ���� �ȿ� ������ ��
        // ��� �����̰� ���� �ȿ� ������ attack�� �ع���
        // ���� escape�� ����, chase�� ���� ���� �۵����� ����
        // ������ ��Ÿ�� ���̴��� �ٷ� attack���� �̵��Ǳ� ������ �ȵ�
        
        // �ذ� ���
        // attack State�� �Ǵ� ���ǿ� (����, ��ų, �ʻ���� )��Ÿ���� �߰��� ��
        
            /*
        if (character.IsWithinstrikingDistance)
        {
            // ����ġ�鼭 ����
            // �߰� ���� �ʿ� ��) �������ٰ� �����ϴ� term
            character.StateMachine.ChangeState(character.AttackState);
        }

             */

        if(character.StateMachine.CurrentPlayerState == character.EscapeState)
        {
            character.MoveTo((-(_targetTransform.position - character.transform.position) * _moveSpeed).normalized, _moveSpeed);
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
