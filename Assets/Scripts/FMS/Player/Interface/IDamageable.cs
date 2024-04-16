using UnityEngine;

public interface IDamageable
{
    void Damage(float damageAmount);

    void Die();

    GameObject DamageTrigger { get; set; }
    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }
    float AttackDamage { get; set; }
}
