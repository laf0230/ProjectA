using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "New Skill Data")]
public class SkillDataSO : ScriptableObject, SkillStatus
{
     public Character SelfCharacter { get; set; }
     public Character Target { get; set; }
    [field: SerializeField] public float CoolTime { get; set; }
    [field: SerializeField] public float SpellingSkillDistance { get; set; }
    [field: SerializeField] public float Damage { get; set; }
    [field: SerializeField] public bool IsArea { get; set; }
    [field: SerializeField] public bool IsPenetration { get; set; }
    [field: SerializeField] public bool IsTracking { get; set; }
    [field: SerializeField] public float SkillRange { get; set; }
    [field: SerializeField] public float Duration { get; set; }
    [field: SerializeField] public GameObject Form { get; set; }
    [field: SerializeField] public float Scope { get; set; }
    [field: SerializeField] public float MotionDelay { get; set; }
}
