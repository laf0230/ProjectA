using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ProjectileBase
{
    private void Update()
    {
        // 일정 범위를 벗어나면 비활성화
        if( Properties.Target == null || !Properties.Target.gameObject.activeSelf)
        {
            Debug.Log("타겟이 사라졌습니다");
            gameObject.SetActive(false);
        }

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

