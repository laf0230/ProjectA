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
            // 인식 범위 내에 적이 없을 경우
            character.StateMachine.ChangeState(character.IdleState);
        }

        // 문제 발견 2++024 04 29
        // Escape에서 Attack을 실행 끝난 이후 Chase로 이동 그 이후 " 바로"  Escape로 변경
        // Attack을 하는 조건 -> 범위 안에 들어왔을 때
        // 어느 상태이건 범위 안에 들어오면 attack을 해버림
        // 따라서 escape의 도망, chase의 추적 등이 작동하지 않음
        // 심지어 쿨타임 중이더라도 바로 attack으로 이동되기 때문에 안됨
        
        // 해결 방법
        // attack State가 되는 조건에 (공격, 스킬, 필살기의 )쿨타임을 추가할 것
        
            /*
        if (character.IsWithinstrikingDistance)
        {
            // 도망치면서 공격
            // 추가 조건 필요 예) 도망가다가 공격하는 term
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
