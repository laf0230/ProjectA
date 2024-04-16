using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SkillStatus
{
    // Skill Status Infomation 
    public float CoolTime { get; set; }
    public float Damage { get; set; }
    public bool IsArea { get; set; }
    public bool IsPenetration { get; set; }
    public float SkillRange { get; set; }
    public float Duration { get; set; }
    public GameObject Form { get; set; }
    public float Scope { get; set; }
    public float MotionDelay { get; set; } // 에니메이션의 총 시간
}
