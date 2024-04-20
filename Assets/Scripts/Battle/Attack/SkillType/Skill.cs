using UnityEngine;

public class Skill : SkillBase
{
    public override void StartAttack()
    {
        base.StartAttack();
    
        Debug.Log("Skill");
    }
}
