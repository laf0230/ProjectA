using UnityEngine;

public class Attack : SkillBase
{
    public override void StartAttack()
    {
        base.StartAttack();

        Debug.Log("Attack");
    }
}
