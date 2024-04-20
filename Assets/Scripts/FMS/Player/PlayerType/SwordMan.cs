using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMan : Character
{
    private new void Awake() 
    {
        base.Awake();

        Attack = gameObject.AddComponent<Slash>();
        Skill = gameObject.AddComponent<Bash>();
        SpecialSkill = gameObject.AddComponent<Throw>();
        
        Attack.enabled = true;
        Skill.enabled = true;
        SpecialSkill.enabled = true;
 
        Attack.skilldata = AttackDataSO;
        Skill.skilldata = SkillDataSO;
        SpecialSkill.skilldata = SpecialSkillDataSO;
    }
}