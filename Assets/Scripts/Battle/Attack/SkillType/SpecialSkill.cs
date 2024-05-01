using UnityEngine;

public class SpecialSkill : SkillBase
{
    public override void StartAttack()
    {   
        base.StartAttack();

        Debug.Log("Special Skill");
        Character.AnimationTriggerEvent(Character.AnimationTriggerType.SpecialSkill);
    }
}