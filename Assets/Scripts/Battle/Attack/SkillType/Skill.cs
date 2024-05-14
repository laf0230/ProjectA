using UnityEngine;

public class Skill : SkillBase
{
    public void S_SA()
    {
        base.StartAttack();
    }

    public void S_EA()
    {
        base.EndAttack();
    }

    public void S_SR()
    {
        base.StartRestriction();
    }

    public void S_ER()
    {
        base.EndRestriction();
    }

    public void S_AT()
    {
        base.AttackTiming();
    }
}
