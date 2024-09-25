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

public enum SkillShapeType
{
    Circle,
    Rectangle
}

[CreateAssetMenu(fileName = "", menuName = "New Skill Data")]
public class SkillDataSO : ScriptableObject
{
    public Profile Profile;
    public SkillType Type;
    public SkillShapeType ShapeType;
    public float SkillSize;
    public int targetType;            // 대상 타입 (단일 대상, 다중 대상 등)
    public float CoolTime;
    public float Damage;
    public float Speed;
    public float Reach;
    public bool hasMovementAction;
    public MovementActionType MovementActionType;
    public float MovementRange;
    public List<AbilityInfo> Ability;
}
