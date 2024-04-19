using UnityEngine;

public class Skill : SkillBase
{
    private void OnEnable()
    {
        SelfCharacter.AnimationTriggerEvent(Character.AnimationTriggerType.Skill);
    }
    public override void StartAttack()
    {
        base.StartAttack();
    
        Debug.Log("Skill");
    }

    public override void AttackTiming()
    {
        base.AttackTiming();

        // BattleManager.instance.
    }
}
