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
public class SkillDataSO : ScriptableObject
{
    public Profile Profile;
    public SkillType Type;
    public SkillShapeType ShapeType;
    public float SkillSize;
    public int targetType;            // ��� Ÿ�� (���� ���, ���� ��� ��)
    public float CoolTime;
    public float Damage;
    public float Speed;
    public float Reach;
    public bool hasMovementAction;
    public MovementActionType MovementActionType;
    public float MovementRange;
    public List<AbilityInfo> Ability;
}
