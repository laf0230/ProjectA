using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBison : Character
{
    public event Action<Character, float> OnKill;

    private new void Start()
    {
        base.Start();

        SetAttack(gameObject.AddComponent<A_RedBison>());
        SetSkill(gameObject.AddComponent<S_Barrage>());
        SetSpecialSkill(gameObject.AddComponent<SS_HunterKiller>());

        Attack.enabled = true;
        Skill.enabled = true;
        SpecialSkill.enabled = true;

        Attack.skilldata = AttackDataSO;
        Skill.skilldata = SkillDataSO;
        SpecialSkill.skilldata = SpecialSkillDataSO;

        // OnKill += BattleManager.Instance.Heal;
        // OnKill.Invoke(this, (MaxHealth * 0.1f));
    }

    private new void Update()
    {
        base.Update();
/*
        if (IsBuffable)
        {
            CurrentHealth += MaxHealth * 0.1f;
            IsBuffable = false;
        }
 */       
    }

    private void DebbugSkill()
    {
        Debug.Log($"Attack: {(Attack.isAttacking ? "Use" : ' ')}");
        Debug.Log($"Attack: {(Attack.isAttacking ? "Use" : ' ')}");
        Debug.Log($"Attack: {(Attack.isAttacking ? "Use" : ' ')}");
    }
}
