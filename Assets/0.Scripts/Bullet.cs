using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Poolable
{
    [SerializeField] GameObject Target;
    [SerializeField] float Speed;
    [SerializeField] float Damage;
    [SerializeField] Rigidbody Rigidbody;
    // 관통 비관통

    public Bullet(GameObject Target, float Speed, float Damage)
    {
        this.Target = Target;
        this.Speed = Speed;
        this.Damage = Damage;
    }

    public void SetData(GameObject Target, float Damage, float Speed = 100f)
    {
        this.Target = Target;
        this.Speed = Speed;
        this.Damage = Damage;
    }

    public void Shoot()
    {
        Vector2 direction = transform.position - Target.transform.position;       

        Rigidbody.AddForce(direction * Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Target)
        {
            Target.GetComponent<Character>().Damage(Damage);
            gameObject.SetActive(false);
        }
    }
}
