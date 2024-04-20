using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
    AttackState�� ����ϸ� { Attack, Skill, SpcialSkill } ��/�� ���Ǿ�� ��
    ���Ǵ� ���� { 1. ��Ÿ�� }
    [��Ÿ��] AttackState���� �ٷ�? ����
    [��Ÿ��] Character���� �ٷ�
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

        // �÷��̾��� ��Ȳ�� ���� ���¸� �����ϴ� �Լ�

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
            // [PROBLOM] �ִϸ��̼��� ������ ���� ���¿��� Ż��� ���°� �����
            // ���� ��������� �������� ���� ��� ����
            Attacking();
            character.StateMachine.ChangeState(character.IdleState);
        }
        else if (_target.gameObject.activeSelf && !character.IsPassThrough)
        {
            Attacking();
            // ���� ��������� �������� ���� ���
            character.StateMachine.ChangeState(character.ChaseState);
        }
        else if (!_target.gameObject.activeSelf)
        {
            Attacking();
            // ���� ������� ���, IDLE ���·� ����
            character.StateMachine.ChangeState(character.IdleState);
        }
*/
    }

    public void Attacking()
    {
       // ��Ÿ�Ӱ� ��� �������� ���� ��ų�� ������ �����ؼ� �����ϴ� �Լ� 
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
