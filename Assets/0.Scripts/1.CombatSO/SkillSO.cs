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
public class SkillSO : ScriptableObject, ISkillData
{
    public Profile Profile { get; set; }
    public SkillType Type{get;set;}
    public SkillShapeType ShapeType{get;set;}
    public float SkillSize{get;set;}
    public int TargetType{get;set;}            // 대상 타입 (단일 대상, 다중 대상 등)
    public float CoolTime{get;set;}
    public int BulletType { get; set; } = 1;
    public List<Transform> Targets { get; set; }
    public float Damage{get;set;}
    public float Speed{get;set;}
    public float Reach{get;set;}
    public bool HasMovementAction{get;set;}
    public MovementActionType MovementActionType{get;set;}
    public TargetMovementLocaction TargetMovementLocaction { get; set; }
    public float MovementRange{get;set;}
    public List<AbilityInfo> Ability{get;set;}
}
