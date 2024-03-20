using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject target;
    public WeaponStats weaponStats = new WeaponStats();
    bool isAttaced = false;
    public Weapon GetWeapon()
    {
        return this;
    }

    public GameObject GetCollidedObject()
    {
        if (target != null)
        {
            return target;
        }
        else
        {
            Debug.Log("Attacking Target is none.");
            return null;
        }
    }

    void AttackTarget()
    {
        var targetcontroller = target.GetComponent<EnemyController>();
        targetcontroller.TakeDamage(weaponStats.attackDamage);
    }

    public void SetTarget(GameObject _target)
    {
        target = _target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.gameObject != gameObject)
        {
            SetTarget(other.gameObject);
        }
    }
}

public class WeaponStats
{
    public float attackDamage = 10f;
    public float knockbackForce = 10f;
    public float attackRange = 2f;
}
