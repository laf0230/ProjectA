using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillSwitcher
{
    attack,
    skill,
    specialSkill
}

public class CharacterAttackState : State
{
    private Transform _transform;
    private Transform _target;
    private Combat Attack;

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

        _target = character.Targets[0].transform;
        foreach(var combat in character.combats)
        {
            // 공격 상태일 때 사용할 스킬 할당
            if(combat.IsUseable())
            {
                Attack = combat;
                Attack.SetTarget(_target);
                break;
            }
        }

        switch(Attack.skillInfo.type)
        {
            case SkillType.Attack:
                character.AnimationTriggerEvent(Character.AnimationTriggerType.Attack);
                break;
            case SkillType.Skill:
                character.AnimationTriggerEvent(Character.AnimationTriggerType.Skill);
                break;
            case SkillType.SpecialSkill:
                character.AnimationTriggerEvent(Character.AnimationTriggerType.SpecialSkill);
                break;
        }
        
        Debug.Log("Skill Type: " + Attack.skillInfo.type);
    }

    public void DoAttack()
    {
        Attack.Use();
    }

    public void DoAbility()
    {
        // 애니메이션에서 특수 능력을 사용하게 하는 트리거
        foreach (var ability in Attack.abilities)
        {
            switch (ability.Info.TargetType)
            {
                case TargetType_.Self:
                    ability.use(_transform);
                    break;
                case TargetType_.Enemy:
                    ability.use(_target);
                    break;
                case TargetType_.Enemies:
                    /*
                    var targets = new List<Transform>();
                    ability.use(targets);
                    */
                    break;
            }
        }
    }

    public void EndAttack()
    {
        Attack.cooldown.ResetCooldown();
        stateMachine.ChangeState(character.ChaseState);
    }
}
