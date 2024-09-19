using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillable
{
    public List<Combat> combats { get; set; } 
    public SkillDataSO AttackDataSO { get; set; }
    public SkillDataSO SkillDataSO { get; set; }
    public SkillDataSO SpecialSkillDataSO { get; set; }
    public bool IsAttackable { get; set; }
    public bool IsBuffable { get; set; }
    
    public void DoAttack(GameObject target, bool direction, float border) { }
}