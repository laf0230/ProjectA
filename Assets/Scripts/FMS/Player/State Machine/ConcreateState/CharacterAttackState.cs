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

        AnimationTriggerEvent(Character.AnimationTriggerType.Attack);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        character.MoveTo(new Vector3(0, 0, 0), 0f);

        if (_target.gameObject.activeSelf && character.IsPassThrough)
        {
            // [PROBLOM] �ִϸ��̼��� ������ ���� ���¿��� Ż��� ���°� �����
            // ���� ��������� �������� ���� ��� ����
            Attacking();
            character.StateMachine.ChangeState(character.EscapeState);
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
    }

    public void Attacking()
    {

            if (SpecialSkill.isAttackable)
            {
                character.gameObject.GetComponent<SpecialSkill>().enabled = true;
            }
            else if (Skill.isAttackable)
            {
                character.gameObject.GetComponent<Skill>().enabled = true;
            }
            else if (Attack.isAttackable)
            {
                character.gameObject.GetComponent<Attack>().enabled = true;
            }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    #region Animation

    public override void EnterAnim()
    {
        base.EnterAnim();


    }

    public void AttackTiming()
    {
        // _target.Damage();
    }

    public override void ExitAnim()
    {
        base.ExitAnim();
    }

    #endregion
}
