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

[CreateAssetMenu(fileName = "", menuName = "New Skill Data")]
public class SkillDataSO : ScriptableObject
{
    public string Name;
    public SkillType Type;
    public int rangeType;             // ��ų ���� Ÿ�� (����, ���Ÿ� ��)
    public int targetType;            // ��� Ÿ�� (���� ���, ���� ��� ��)
    public float CoolTime;
    public float Damage;
    public float Speed;
    public List<AbilityInfo> Ability;
}
