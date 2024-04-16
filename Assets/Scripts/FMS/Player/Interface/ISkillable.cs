using System.Collections;
using UnityEngine;

public interface ISkillable
{
    public Attack Attack { get; set; }
    public Skill Skill { get; set; }
    public SpcialSkill SpcialSkill { get; set; }
}