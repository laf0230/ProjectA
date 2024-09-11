using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject Target;
    float Speed;
    float Damage;
    Rigidbody Rigidbody;
    // 관통 비관통

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
}
