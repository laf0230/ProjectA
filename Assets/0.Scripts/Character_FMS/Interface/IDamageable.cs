using System.Collections;
using UnityEngine;

public interface IDamageable
{
    void Damage(float damageAmount);

    IEnumerator Die();

    float CurrentHealth { get; set; }
}
