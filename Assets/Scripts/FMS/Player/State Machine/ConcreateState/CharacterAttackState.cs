using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
    애니메이션 실행으로 사용
 */

public enum SkillSwitcher
{
    attack,
    skill,
    specialSkill
}

public class CharacterAttackState : State
{
    private Transform _transform;
    private Character _target;

    public Attack Attack;
    public Skill Skill;
    public SpecialSkill SpecialSkill;

    public bool isAttacking = false;

    public Queue<SkillSwitcher> skillQueue = new Queue<SkillSwitcher>();
    SkillBase currentSkill;

    public CharacterAttackState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        _transform = character.transform;
    }

    public override void AnimationTriggerEvent(Character.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log(".............................................");

        Attack = character.Attack;
        Skill = character.Skill;
        SpecialSkill = character.SpecialSkill;
        character.IsMoveable = false;

        #region Enqueue Skills

        if (SpecialSkill.isAttackable)
        {
            skillQueue.Enqueue(SkillSwitcher.specialSkill);
        }

        if(Skill.isAttackable)
        {
            skillQueue.Enqueue(SkillSwitcher.skill);
        }

        if (Attack.isAttackable)
        {
            skillQueue.Enqueue(SkillSwitcher.attack);
        }

        #endregion 

        // for Skill State
        _target = character.Target.GetComponent<Character>();

        switch (skillQueue?.Dequeue())
        {
            case SkillSwitcher.specialSkill:
                currentSkill = SpecialSkill;
                character.AnimationTriggerEvent(Character.AnimationTriggerType.SpecialSkill);
                break;

            case SkillSwitcher.skill:
                currentSkill = Skill;
                character.AnimationTriggerEvent(Character.AnimationTriggerType.Skill);
                break;

            case SkillSwitcher.attack:
                currentSkill = Attack;
                character.AnimationTriggerEvent(Character.AnimationTriggerType.Attack);
                break;
        }
        currentSkill.isAttacking = true;
        // Debug.Log("1st ----- I'll Shot this: " + currentSkill);
        currentSkill.isAttackable = false;
        // Debug.Log("2nd ----- I'll Shot this: " + currentSkill);
    }

    public override void ExitState()
    {
        base.ExitState();

        character.IsMoveable = true;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        character.CheckForLeftOrRightFacing(_target.transform.position - _transform.position);
        character.MoveTo(Vector3.zero);

        if (currentSkill.isAttacking == false)
        {
            character.StateMachine.ChangeState(character.ChaseState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}