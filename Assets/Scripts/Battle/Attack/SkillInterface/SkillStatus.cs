using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SkillStatus
{
    // 스킬에 필수적인 요소 설계

    public Character SelfCharacter { get; set; }
    public Character Target { get; set; }
    public float CoolTime { get; set; } // 쿨타임
    public float Damage { get; set; } // 데미지
    public bool IsArea { get; set; } // 광범위 여부
    public bool IsPenetration { get; set; } // 관통 여부
    public float SkillRange { get; set; } // 스킬 범위
    public float Duration { get; set; } // 스킬 지속 시간
    public GameObject Form { get; set; } // 스킬 형태
    public float Scope { get; set; } // 스킬 범위
    public float MotionDelay { get; set; } // 에니메이션의 총 시간
}
