using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletProperties : IBulletData
{
    [SerializeField] public int Type { get; set; }
    [SerializeField] public float Damage{get;set;}
    [SerializeField] public float Speed{get;set;}
    [SerializeField] public float Reach{get;set;}
    
    [SerializeField] public Transform Target{get;set;}
    [SerializeField] public Transform User{get;set;}

    public BulletProperties(int bulletType, float damage, float speed, Transform user, float reach)
    {
        Type = bulletType;
        Damage = damage;
        Speed = speed;
        User = user;
        Reach = reach;
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
    public int BulletType{get;set;}
    public List<Transform> Targets { get; set;} = new List<Transform>();
    public float Damage{get;set;}
    public float Speed{get;set;}
    public float Reach{get;set;}
    public BulletProperties BulletProperties{get;set;}
    public List<AbilityInfo> Ability{get;set;} = new List<AbilityInfo>();

    public float SkillSize {get;set;}
    public bool HasMovementAction {get;set;}
    public MovementActionType MovementActionType {get;set;}
    public float MovementRange {get;set;}

    public SkillProperties(Transform user, SkillType type, SkillShapeType shapeType, int targetType, float totalCoolTime,int bulletType, float Damage, float Speed, float Reach, List<AbilityInfo> abilityInfos)
    {
        this.user = user;
        this.Type = type;
        this.ShapeType = shapeType;
        this.TargetType = targetType;
        this.CoolTime = totalCoolTime;
        this.BulletType = bulletType;

        this.Damage = Damage;
        this.Speed = Speed;
        this.Reach = Reach;

        BulletProperties = new BulletProperties(bulletType, Damage, Speed, user, Reach);
        this.BulletProperties.Damage = Damage;
        this.BulletProperties.Speed = Speed;
        this.BulletProperties.Reach = Reach;
        Ability = abilityInfos;
    }

    public void SetTargets(List<Transform> targets)
    {
        this.Targets = targets;
        BulletProperties.SetTarget(targets[0]);
    }
}