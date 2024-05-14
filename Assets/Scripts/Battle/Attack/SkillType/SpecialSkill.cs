using UnityEngine;

public class SpecialSkill : SkillBase
{
    public void SS_SA()
    {
        base.StartAttack();
    }

    public void SS_EA()
    {
        base.EndAttack();
    }

    public void SS_SR()
    {
        base.StartRestriction();
    }

    public void SS_ER()
    {
        base.EndRestriction();
    }

    public void SS_AT()
    {
        base.AttackTiming();
    }
}