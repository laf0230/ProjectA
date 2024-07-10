using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    Poison,
    Invisible,
    Heal,
    EnHancedForce,
    EnHancedSpeed,
    Slowdown,
    Silence
}

public interface IBuffConditions
{
    bool IsHit { get; set; }
    float Duration { get; set; }
    float Damage { get; set; }

    public void ApplyBuff(BuffType buffType) { } 
}
