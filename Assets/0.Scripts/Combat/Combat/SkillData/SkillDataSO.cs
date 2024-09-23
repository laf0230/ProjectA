using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Attack,
    Skill,
    Ultimate,
    Passive
}

public enum StatusList
{
    Health, // 체력
    Speed, // 이동속도
    AttackSpeed, // 공격 속도
}

[CreateAssetMenu(fileName = "", menuName = "New Skill Data")]
public class SkillDataSO : ScriptableObject
{
    public string Name;
    public SkillType Type;
    public int rangeType;             // 스킬 범위 타입 (근접, 원거리 등)
    public int targetType;            // 대상 타입 (단일 대상, 다중 대상 등)
    public float CoolTime;
    public float Damage;
    public float Speed;
    public List<AbilityInfo> Ability;
}
