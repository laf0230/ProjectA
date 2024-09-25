using System.Collections.Generic;
using UnityEngine;

public class BulletInfo
{

    public float Damage;
    public float Speed;
    public float Reach;

    public Transform User;
    public Transform Target;

    public BulletInfo(float damage, float speed, Transform user, float reach)
    {
        Damage = damage;
        Speed = speed;
        User = user;
        Reach = reach;
    }

    public void SetTarget(Transform target) { Target = target; }
    public void setUser(Transform user) { User = user; }
}

public class SkillInfo
{
    public Profile Profile;
    public SkillType type;               // ��ų ��� (������ ������ �� ����)
    public SkillShapeType shapeType;             // ��ų ���� Ÿ�� (����, ���Ÿ� ��)
    public int targetType;            // ��� Ÿ�� (���� ���, ���� ��� ��)
    public float totalCoolTime;            // �浹 �ð� �Ǵ� ������
    public BulletInfo bulletInfo;
    public List<AbilityInfo> abilityInfos; // ��ų�� ���õ� �ɷ� ����Ʈ

    public SkillInfo(SkillType type, SkillShapeType shapeType, int targetType, float totalCoolTime, BulletInfo bulletInfo, List<AbilityInfo> abilityInfos)
    {
        this.type = type;
        this.shapeType = shapeType;
        this.targetType = targetType;
        this.totalCoolTime = totalCoolTime;
        this.bulletInfo = bulletInfo;
        this.abilityInfos = abilityInfos;
    }
}