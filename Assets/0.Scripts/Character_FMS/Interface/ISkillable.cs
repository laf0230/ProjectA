using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillable
{
    public List<Combat> combats { get; set; } 
    public SkillSO AttackDataSO { get; set; }
    public SkillSO SkillDataSO { get; set; }
    public SkillSO SpecialSkillDataSO { get; set; }
    public bool IsAttackable { get; set; }
    public bool IsBuffable { get; set; }
    
    public void DoAttack(GameObject target, bool direction, float border) { }
}