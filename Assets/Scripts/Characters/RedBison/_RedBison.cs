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

    private new void Update()
    {
        base.Update();

    }

    private void DebbugSkill()
    {
        Debug.Log($"Attack: {(Attack.isAttacking ? "Use" : ' ')}");
        Debug.Log($"Attack: {(Attack.isAttacking ? "Use" : ' ')}");
        Debug.Log($"Attack: {(Attack.isAttacking ? "Use" : ' ')}");
    }
}
