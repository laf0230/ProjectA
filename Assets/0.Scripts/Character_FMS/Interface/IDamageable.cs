using UnityEngine;

public interface IDamageable
{
    void Damage(float damageAmount);

    void Die();

    float CurrentHealth { get; set; }
}
