﻿using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    public BulletProperties Properties { get; set; } = new BulletProperties();
    protected Rigidbody rb;
    protected Vector3 direction;
    public ProjectileType type;

    public virtual void Initialize(BulletProperties properties)
    {
        Debug.Log("투사체의 프로퍼티가 설정되었습니다");
        Properties = properties;
        rb = GetComponent<Rigidbody>();
    }

    public void SetTarget(Transform target)
    {
        Properties.SetTarget(target);
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    public virtual void Shoot()
    {
        Debug.Log(Properties + "프로퍼티는 과연 설정되었는가요?");
        if (!IsReadyToShoot())
        {
            // 데이터 설정에 실패했을 때 예외처리
            Debug.LogError("Projectile is missing required data.");
            return;
        }

        SetDirection(Properties.Target.position - transform.position);
        rb.AddForce(direction * Properties.Speed, ForceMode.Impulse);
    }

    protected bool IsReadyToShoot()
    {
        return Properties.User != null && Properties.Target != null && Properties.Speed > 0 && Properties.Damage != 0;
    }

    protected abstract void OnImpact(Collider other);

    protected void ApplyDamage(Collider targetCollider)
    {
        var character = targetCollider.GetComponent<Character>();
        if (character != null)
        {
            character.Damage(Properties.Damage);
        }

        if(character.CurrentHealth < 0)
        {
            var user = Properties.User.gameObject.GetComponent<Character>().Info;
        }
    }
}



