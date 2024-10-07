using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillData
{
    Profile Profile { get; set; }
    SkillType Type {get; set;}
    SkillShapeType ShapeType {get; set;}
    float SkillSize {get; set;}
    int TargetType {get; set;}            // ��� Ÿ�� (���� ���, ���� ��� ��)
    float CoolTime {get; set;}
    int BulletType { get; set; }
    List<Transform> Targets {get; set;}
    float Damage {get; set;}
    float Speed {get; set;}
    float Reach {get; set;}
    bool HasMovementAction {get; set;}
    MovementActionType MovementActionType {get; set;}
    TargetMovementLocaction TargetMovementLocaction {get; set;}
    float MovementRange {get; set;}
    List<AbilityInfo> Ability {get; set;}
}

public enum TargetMovementLocaction
{
    ToEnemy,
    OppositeToEnemy,
    Random
}
