using System.Collections.Generic;
using UnityEngine;

public class SkillInfo
{
    public Profile Profile;
    public SkillType type;               // 스킬 등급 (강도를 결정할 수 있음)
    public SkillShapeType shapeType;             // 스킬 범위 타입 (근접, 원거리 등)
    public int targetType;            // 대상 타입 (단일 대상, 다중 대상 등)
    public float totalCoolTime;            // 충돌 시간 또는 딜레이
    public BulletProperties bulletInfo;
    public List<AbilityInfo> abilityInfos; // 스킬과 관련된 능력 리스트
    public float Damage;
    public float Speed;
    public float Reach;

    public SkillInfo(Transform user, float Damage, float Speed,float Reach, SkillType type, SkillShapeType shapeType, int targetType, float totalCoolTime, BulletProperties bulletInfo, List<AbilityInfo> abilityInfos)
    {
    }
}

public class BulletProperties
{
    // 관통 여부
    public bool IsPiercing;
    // 단일 여부
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