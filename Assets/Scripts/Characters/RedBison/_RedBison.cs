using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _RedBison : Character
{
    private new void Awake()
    {
        base.Awake();

        Attack = gameObject.AddComponent<Attack>();
        Skill = gameObject.AddComponent<S_Barrage>();
        SpecialSkill = gameObject.AddComponent<SS_HunterKiller>();

        Attack.enabled = true;
        Skill.enabled = true;
        SpecialSkill.enabled = true;

        Attack.skilldata = AttackDataSO;
        Skill.skilldata = SkillDataSO;
        SpecialSkill.skilldata = SpecialSkillDataSO;
    }
}
