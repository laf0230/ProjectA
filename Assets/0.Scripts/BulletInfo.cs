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
    public string Name;               // ��ų �̸�
    public SkillType type;               // ��ų ��� (������ ������ �� ����)
    public int rangeType;             // ��ų ���� Ÿ�� (����, ���Ÿ� ��)
    public int targetType;            // ��� Ÿ�� (���� ���, ���� ��� ��)
    public float totalCoolTime;            // �浹 �ð� �Ǵ� ������
    public BulletInfo bulletInfo;
    public List<AbilitySO> abilities; // ��ų�� ���õ� �ɷ� ����Ʈ

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