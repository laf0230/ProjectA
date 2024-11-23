using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ProjectileBase
{
    private void Update()
    {
        // ���� ������ ����� ��Ȱ��ȭ
        float distance = Vector3.Distance(Properties.Target.transform.position, Properties.User.position);
        if (distance > Properties.Reach + 1.5f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnImpact(other);
    }

    protected override void OnImpact(Collider other)
    {
        if (other.gameObject == Properties.Target.gameObject)
        {
            ApplyDamage(other);
            gameObject.SetActive(false);
        }
    }
}

