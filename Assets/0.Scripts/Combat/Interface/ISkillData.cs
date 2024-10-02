using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillData
{
    public Profile Profile { get; set; }
    public SkillType Type {get; set;}
    public SkillShapeType ShapeType {get; set;}
    public float SkillSize {get; set;}
    public int TargetType {get; set;}            // 대상 타입 (단일 대상, 다중 대상 등)
    public float CoolTime {get; set;}
    public int BulletType { get; set; }
    public List<Transform> Targets {get; set;}
    public float Damage {get; set;}
    public float Speed {get; set;}
    public float Reach {get; set;}
    public bool HasMovementAction {get; set;}
    public MovementActionType MovementActionType {get; set;}
    public TargetMovementLocaction TargetMovementLocaction {get; set;}
    public float MovementRange {get; set;}
    public List<AbilityInfo> Ability {get; set;}
}

public enum TargetMovementLocaction
{
    ToEnemy,
    OppositeToEnemy,
    Random
}
