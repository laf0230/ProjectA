using UnityEngine;

public class Attack : SkillBase
{
    public void A_SA()
    {
        base.StartAttack();
        Debug.Log("A_SA");
    }

    public void A_EA()
    {
        base.EndAttack();
    }

    public void A_SR()
    {
        base.StartRestriction();
    }

    public void A_ER()
    {
        base.EndRestriction();
    }

    public void A_AT()
    {
        base.AttackTiming();
    }
}
