using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
    AttackState를 사용하면 { Attack, Skill, SpcialSkill } 이/가 사용되어야 함
    사용되는 조건 { 1. 쿨타임 }
    [쿨타임] AttackState에서 다룸? ㄴㄴ
    [쿨타임] Character에서 다룸
 */

public class CharacterAttackState : State
{
    private Transform _transform;
    private Character _target;

    public SkillState SkillState;
    public Attack Attack;
    public Skill Skill;
    public SpecialSkill SpecialSkill;

    public CharacterAttackState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        _transform = GameObject.FindGameObjectWithTag("Character").transform;
    }

    public override void AnimationTriggerEvent(Character.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        // for Skill State
        Attack = character.Attack;
        Skill = character.Skill;
        SpecialSkill = character.SpecialSkill;

        _target = character.Target.GetComponent<Character>();

    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        // 플레이어의 상황에 따라서 상태를 변경하는 함수

        if (_target.gameObject.activeSelf)
        {
            if (SpecialSkill.isAttackable || Skill.isAttackable || Attack.isAttackable)
            {
            Attacking();
            }
        }

        if (SpecialSkill.isAttacking || Skill.isAttacking || Attack.isAttacking)
        {
            character.CheckForLeftOrRightFacing(_target.transform.position -  _transform.position);
        } 
        else if (!SpecialSkill.isAttacking && !Skill.isAttacking && !Attack.isAttacking)
        {
            character.StateMachine.ChangeState(character.ChaseState);
        }

        /*
        if (_target.gameObject.activeSelf && character.IsPassThrough)
        {
            // [PROBLOM] 애니메이션이 끝나지 않은 상태에서 탈출로 상태가 변경됨
            // 적이 살아있으나 위협도가 높은 경우 도망
            Attacking();
            character.StateMachine.ChangeState(character.IdleState);
        }
        else if (_target.gameObject.activeSelf && !character.IsPassThrough)
        {
            Attacking();
            // 적이 살아있으나 위협도가 낮은 경우
            character.StateMachine.ChangeState(character.ChaseState);
        }
        else if (!_target.gameObject.activeSelf)
        {
            Attacking();
            // 적이 사망했을 경우, IDLE 상태로 변경
            character.StateMachine.ChangeState(character.IdleState);
        }
*/
    }

    public void Attacking()
    {
       // 쿨타임과 사용 중인지에 따라서 스킬의 종류를 변경해서 공격하는 함수 
        if (SpecialSkill.isAttackable && !Skill.isAttackable)
        {
            character.AnimationTriggerEvent(Character.AnimationTriggerType.SpecialSkill);
            SpecialSkill.ResetCoolTime();
        }
        else if (Skill.isAttackable && !SpecialSkill.isAttackable)
        {
            character.AnimationTriggerEvent(Character.AnimationTriggerType.Skill);
            Skill.ResetCoolTime();
        }
        else if (Attack.isAttackable)
        {
            character.AnimationTriggerEvent(Character.AnimationTriggerType.Attack);
            Attack.ResetCoolTime();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
