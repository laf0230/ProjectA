using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : SkillState, SkillStatus 
{
    public float CoolTime { get; set; }
    public float Damage { get; set; }
    public bool IsArea { get; set; }
    public bool IsPenetration { get; set; }
    public float SkillRange { get; set; }
    public float Duration { get; set; }
    public GameObject Form { get; set; }
    public float Scope { get; set; }
    public float MotionDelay { get; set; }
    // Basic Skill Running
    // AttackState���� attack, skill, SS�� enum�� ���ؼ� ����� �� �ٲٴ� ���

    #region AnimationFunc

    public override void StartAttack()
    {
        base.StartAttack();

        Debug.Log("StartAttack");
    }

    public override void EndAttack()
    {
        base.EndAttack();
    }

    public override void AttackTiming()
    {
        base.AttackTiming();
    }

    public override void StartRestriction()
    {
        base.StartRestriction();
    }

    public override void EndRestriction()
    {
        base.EndRestriction();
    }

    #endregion
}
