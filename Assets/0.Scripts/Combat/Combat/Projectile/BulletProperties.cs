using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletProperties : IBulletData
{
    [SerializeField] public ProjectileType Type { get; set; }
    [SerializeField] public float Damage{get;set;}
    [SerializeField] public float Speed{get;set;}
    [SerializeField] public float Reach{get;set;}
    
    [SerializeField] public Transform Target{get;set;}
    [SerializeField] public Transform User{get;set;}
    [SerializeField] public int Duration { get;set;} // 장판형일경우의 지속시간
    [SerializeField] public bool isUsedOwnPlace { get; set; } = false;

    public BulletProperties( ProjectileType projectileType, float damage, float speed, Transform user, float reach,bool isUsedOwnPlace, int duration)
    {
        Type = projectileType;
        Damage = damage;
        Speed = speed;
        User = user;
        Reach = reach;
        Duration = duration;
        this.isUsedOwnPlace = isUsedOwnPlace;
    }


    public void SetTarget(Transform target) { this.Target = target; }
    public void setUser(Transform user) { User = user; }
}

[System.Serializable]
public class SkillProperties: ISkillData
{
    public Profile Profile { get; set; }
    public Transform user { get; set; }
    public SkillType Type{get;set;}
    public SkillShapeType ShapeType{get;set;}
    public int TargetType{get;set;}
    public float CoolTime{get;set;}
    public ProjectileType ProjectileType {get;set;}
    public List<Transform> Targets { get; set;} = new List<Transform>();
    public float Damage{get;set;}
    public float Speed{get;set;}
    public float Reach{get;set;}
    public BulletProperties BulletProperties{get;set;}
    public List<AbilityInfo> Ability{get;set;} = new List<AbilityInfo>();

    public float SkillSize {get;set;}
    public bool HasMovementAction {get;set;}
    public MovementActionType MovementActionType {get;set;}
    public TargetMovementLocaction TargetMovementLocaction { get; set; }
    public float MovementRange {get;set;}
    public bool isUsedOwnPlace { get; set; } = false;
    public int duration;

    public SkillProperties(
        Transform user,
        SkillType type,
        SkillShapeType shapeType,
        int targetType,
        float totalCoolTime,
        ProjectileType projectileType,
        float Damage,
        float Speed,
        float Reach,
        List<AbilityInfo> abilityInfos,
        bool isUsedOwnPlace,
        int duration
        )
    {
        this.user = user;
        this.Type = type;
        this.ShapeType = shapeType;
        this.TargetType = targetType;
        this.CoolTime = totalCoolTime;
        this.ProjectileType = projectileType;

        this.Damage = Damage;
        this.Speed = Speed;
        this.Reach = Reach;

        BulletProperties = new BulletProperties(projectileType, Damage, Speed, user, Reach, isUsedOwnPlace, duration);
        this.BulletProperties.Damage = Damage;
        this.BulletProperties.Speed = Speed;
        this.BulletProperties.Reach = Reach;
        Ability = abilityInfos;

        this.isUsedOwnPlace = isUsedOwnPlace;
        this.duration = duration;
    }

    public void SetTargets(List<Transform> targets)
    {
        this.Targets = targets;
        BulletProperties.SetTarget(targets[0]);
    }
}