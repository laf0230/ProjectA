using UnityEngine;

public class SpecialSkill : SkillBase
{
    public virtual void SS_SA()
    {
        base.StartAttack();
    }

    public virtual void SS_EA()
    {
        base.EndAttack();
    }

    public virtual void SS_SR()
    {
        base.StartRestriction();
    }

    public virtual void SS_ER()
    {
        base.EndRestriction();
    }

    public virtual void SS_AT()
    {
        base.AttackTiming();
    }
}