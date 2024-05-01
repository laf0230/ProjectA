using System.Collections;
using UnityEngine;

public interface ISkillable
{
    public Attack Attack { get; set; }
    public Skill Skill { get; set; }
    public SpecialSkill SpecialSkill { get; set; }
    public SkillDataSO AttackDataSO { get; set; }
    public SkillDataSO SkillDataSO { get; set; }
    public SkillDataSO SpecialSkillDataSO { get; set; }
    public bool IsAttackable { get; set; }
}