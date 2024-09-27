using System.Collections.Generic;
using UnityEngine;

public class SkillInfo
{
    public Profile Profile;
    public SkillType type;               // ��ų ��� (������ ������ �� ����)
    public SkillShapeType shapeType;             // ��ų ���� Ÿ�� (����, ���Ÿ� ��)
    public int targetType;            // ��� Ÿ�� (���� ���, ���� ��� ��)
    public float totalCoolTime;            // �浹 �ð� �Ǵ� ������
    public BulletProperties bulletInfo;
    public List<AbilityInfo> abilityInfos; // ��ų�� ���õ� �ɷ� ����Ʈ
    public float Damage;
    public float Speed;
    public float Reach;

    public SkillInfo(Transform user, float Damage, float Speed,float Reach, SkillType type, SkillShapeType shapeType, int targetType, float totalCoolTime, BulletProperties bulletInfo, List<AbilityInfo> abilityInfos)
    {
    }
}

public class BulletProperties
{
    // ���� ����
    public bool IsPiercing;
    // ���� ����
    public bool IsSingle;
    public float Damage;
    public float Speed;
    public float Reach;
    
    public List<Transform> Targets;
    public Transform User;

    public BulletProperties(bool isPiercing, bool isSingle, float damage, float speed, Transform user, float reach)
    {
        IsPiercing = isPiercing;
        IsSingle = isSingle;
        Damage = damage;
        Speed = speed;
        User = user;
        Reach = reach;
    }


    public void SetTarget(Transform target) { Targets.Insert(0, target); }
    public void SetTarget(List<Transform> targets) { Targets = targets; }
    public void setUser(Transform user) { User = user; }
}

public class SkillProperties
{
    public Transform user;
    public SkillType type;
    public SkillShapeType shapeType;
    public int targetType;
    public float totalCoolTime;
    public float Damage;
    public float Speed;
    public float Reach;
    public List<AbilityInfo> AbilityInfos;

    public SkillProperties(Transform user, SkillType type, SkillShapeType shapeType, int targetType, float totalCoolTime, float Damage, float Speed, float Reach, List<AbilityInfo> abilityInfos)
    {
        this.user = user;
        this.type = type;
        this.shapeType = shapeType;
        this.targetType = targetType;
        this.totalCoolTime = totalCoolTime;
        this.Damage = Damage;
        this.Speed = Speed;
        this.Reach = Reach;
        AbilityInfos = abilityInfos;
    }
}