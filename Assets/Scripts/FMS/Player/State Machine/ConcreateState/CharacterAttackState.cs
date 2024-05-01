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

    Queue<SkillBase> skillQueue = new Queue<SkillBase>();
    SkillBase currentSkill;

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
        skillQueue.Clear();
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
            if (SpecialSkill.isAttacking || Skill.isAttacking || Attack.isAttacking)
            {
                character.CheckForLeftOrRightFacing(_target.transform.position - _transform.position);
            }
        }

        // Stack ableSkills
        if (SpecialSkill.isAttackable)
        {
            skillQueue.Enqueue(SpecialSkill);
        }

        if (Skill.isAttackable)
        {
            skillQueue.Enqueue(Skill);
        }

        if (Attack.isAttackable)
        {
            skillQueue.Enqueue(Attack);
        }

        // Excute Attack
        if (skillQueue.TryDequeue(out currentSkill) && currentSkill.isAttackable)
        {
            currentSkill.StartAttack();
            Debug.Log($"CurrentSkill: {currentSkill} ");
        }
        else
        {
            character.StateMachine.ChangeState(character.ChaseState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
