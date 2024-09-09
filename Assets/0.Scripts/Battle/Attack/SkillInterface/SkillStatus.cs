using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SkillStatus
{
    // ��ų�� �ʼ����� ��� ����

    public Character SelfCharacter { get; set; }
    public Character Target { get; set; }
    public float CoolTime { get; set; } // ��Ÿ��
    public float SpellingSkillDistance { get; set; } // ��ų �����Ÿ�
    public float Damage { get; set; } // ������
    public bool IsArea { get; set; } // ������ ����
    public bool IsPenetration { get; set; } // ���� ����
    public bool IsTracking { get; set; } // ���� ����
    public float SkillRange { get; set; } // ��ų ����
    public float Duration { get; set; } // ��ų ���� �ð�
    public GameObject bulletPrefab { get; set; } // ��ų ����
    public float Scope { get; set; } // ��ų ����
    public float MotionDelay { get; set; } // ���ϸ��̼��� �� �ð�
}
