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

        // 플레이어의 상황에 따라서 상태를 변경하는 함수

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
