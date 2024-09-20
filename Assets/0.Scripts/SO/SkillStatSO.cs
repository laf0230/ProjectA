using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "New SkillStat")]
public class SkillStatSO: ScriptableObject
{
    public string Name;
    public string Damage;
    public string Rank;
    public int RangeType;
    public int TargetType;
    public float CollTime;
    public List<AbilityInfo> Abilities;
}