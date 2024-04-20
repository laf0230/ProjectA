using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMan : Character
{
    Bash attack;
    Slash skill;
    Throw specialSkill;

    private void Awake() 
    {
        /*
        attack = gameObject.AddComponent<Bash>();
        skill = gameObject.AddComponent<Slash>();
        specialSkill = gameObject.AddComponent<Throw>();
        */
    }

    private void Start()
    {
        /*
        attack.enabled = true;
        skill.enabled = true;
        specialSkill.enabled = true;
 
        attack.skilldata = AttackDataSO;
        skill.skilldata = SkillDataSO;
        specialSkill.skilldata = SpecialSkillDataSO;
        */
    }
}