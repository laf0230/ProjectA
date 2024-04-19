using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTransfer : MonoBehaviour
{
    private Character character;
    public Character targetCharacter;
    public float damage;

    SkillDataSO

    private void Start()
    {
        character = GetComponentInParent<Character>();
        damage = character.AttackDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != character.gameObject && other.CompareTag("Character"))
        {
            targetCharacter = other.gameObject.GetComponent<Character>();

            targetCharacter.Damage(damage);
        }
        Destroy(this.gameObject);
    }


}
