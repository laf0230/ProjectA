using System.Collections;
using UnityEngine;

public interface ISkillable
{
    public Attack Attack { get; set; }
    public Skill Skill { get; set; }
    public SpecialSkill SpecialSkill { get; set; }
    public float AttackCoolTime { get; set; }
    public bool IsAttackable { get; set; }
    public bool IsSkillable { get; set; }
    public bool IsSpcialSkillable { get; set; }
}