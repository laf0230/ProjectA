using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        character.SetMoveAble(false);
        
        _target = character.Targets[0].transform;
        foreach(var combat in character.combats)
        {
            // 공격 상태일 때 사용할 스킬 할당
            if(combat.IsUseable())
            {
                Attack = combat;

                List<Transform> targetTransforms = new List<Transform>(); // Transform을 저장할 리스트 생성

                for (int i = 0; i < character.Targets.Count; i++)
                {
                    GameObject targetCharacter = character.Targets[i];  // 배열에서 GameObject를 가져옵니다.
                    targetTransforms.Add(targetCharacter.transform);  // Transform을 리스트에 추가합니다.
                }

                Attack.SetTarget(targetTransforms);  // 리스트를 SetTargets에 전달합니다
                break;
            }
        }

        switch(Attack.skillProperties.Type)
        {
            case SkillType.Attack:
                character.AnimationTriggerEvent(Character.AnimationTriggerType.Attack);
                break;
            case SkillType.Skill:
                character.AnimationTriggerEvent(Character.AnimationTriggerType.Skill);
                break;
            case SkillType.Ultimate:
                character.AnimationTriggerEvent(Character.AnimationTriggerType.SpecialSkill);
                break;
        }
        
        Debug.Log("Skill Type: " + Attack.skillProperties.Type);
    }

    public void SetTarget(List<Transform> targets)
    {
        // 타겟을 정하는 코드
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
                    var targets = new List<Transform>();
                    ability.use(targets);
                    break;
            }
        }
    }

    public void DoMove()
    {
        // 에니메이션에서 움직임 관련된 내용을 사용하는 트리거
        foreach (var ability in Attack.abilities)
        {
            switch (ability.Info.MovementActionType)
            {
                case MovementActionType.Teleport:
                    Attack.MovementAction(MovementActionType.Teleport);
                    break;
                case MovementActionType.Dash:
                    Attack.MovementAction(MovementActionType.Dash);
                    break;
            }
        }
    }

    public void EndAttack()
    {
        Attack.cooldown.ResetCooldown();
        character.SetMoveAble(true);

        stateMachine.ChangeState(character.ChaseState);
    }
}
