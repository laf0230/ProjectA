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
    Health, // ü��
    Speed, // �̵��ӵ�
    AttackSpeed, // ���� �ӵ�
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
    public int TargetType{get;set;}            // ��� Ÿ�� (���� ���, ���� ��� ��)
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
