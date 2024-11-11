using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    protected BulletProperties Properties { get; set; }
    protected Rigidbody rb;
    protected Vector3 direction;

    public virtual void Initialize(BulletProperties properties)
    {
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
    }
}



