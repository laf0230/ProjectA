using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Character character;
    public Character targetCharacter;
    public float damage;

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
