using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMan : Character
{
    private new void Awake() 
    {
        base.Awake();

        Attack = gameObject.AddComponent<S_Slash>();
        Skill = gameObject.AddComponent<A_Bash>();
        SpecialSkill = gameObject.AddComponent<SS_Throw>();
        
        Attack.enabled = true;
        Skill.enabled = true;
        SpecialSkill.enabled = true;
 
        Attack.skilldata = AttackDataSO;
        Skill.skilldata = SkillDataSO;
        SpecialSkill.skilldata = SpecialSkillDataSO;
    }
}