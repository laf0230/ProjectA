using System.Collections.Generic;
using UnityEngine;

public class BulletInfo
    {
    
        public float Damage;
        public float Speed;
    
        public Transform User;
        public Transform Target;
    
        public BulletInfo(float damage, float speed, Transform user)
        {
            Damage = damage;
            Speed = speed;
            User = user;
        }
    
        public void SetTarget(Transform target) {  Target = target; }
        public void setUser(Transform user) { User = user; }
    }

public class SkillInfo
{
    public string Name;               // 스킬 이름
    public SkillType type;               // 스킬 등급 (강도를 결정할 수 있음)
    public int rangeType;             // 스킬 범위 타입 (근접, 원거리 등)
    public int targetType;            // 대상 타입 (단일 대상, 다중 대상 등)
    public float totalCoolTime;            // 충돌 시간 또는 딜레이
    public BulletInfo bulletInfo;
    public List<AbilitySO> abilities; // 스킬과 관련된 능력 리스트

    public SkillInfo(string name, SkillType type, int rangeType, int targetType, float totalCoolTime, BulletInfo bulletInfo, List<AbilitySO> abilities)
    {
        Name = name;
        this.type = type;
        this.rangeType = rangeType;
        this.targetType = targetType;
        this.totalCoolTime = totalCoolTime;
        this.bulletInfo = bulletInfo;
        this.abilities = abilities;
    }
}