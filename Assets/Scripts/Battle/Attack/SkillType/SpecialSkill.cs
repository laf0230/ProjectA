using UnityEngine;

public class SpecialSkill : SkillBase
{
    private void OnEnable()
    {
        SelfCharacter.AnimationTriggerEvent(Character.AnimationTriggerType.SpecialSkill);
    }

    public override void StartAttack()
    {
        base.StartAttack();

        Debug.Log("Special Skill");
    }
}