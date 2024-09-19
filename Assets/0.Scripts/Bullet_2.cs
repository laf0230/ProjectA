using UnityEngine;

public class Bullet_2 : Poolable
{
    private BulletInfo combatInfo;
    private Transform user;
    private Transform target;
    public float speed;
    public float damage;
    public Vector3 direction;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsValidTarget(other))
        {
            ApplyDamage(other);
            gameObject.SetActive(false);
        }
    }

    public void Initialize(BulletInfo combatInfo)
    {
        this.combatInfo = combatInfo;
        SetUser(combatInfo.User);
        SetTarget(combatInfo.Target);
        SetSpeed(combatInfo.Speed);
        SetDamage(combatInfo.Damage);
    }

    public void SetCombatInfo(BulletInfo combatInfo)
    {
        this.combatInfo = combatInfo;
    }

    public void SetUser(Transform user)
    {
        this.user = user;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetSpeed(float speed = 10f)
    {
        this.speed = speed;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void Shoot()
    {
        if (!IsReadyToShoot())
        {
            Debug.LogError("Bullet is missing required data: user, target, speed, or damage.");
            return;
        }

        CalculateDirection();
        AddForceToRigidbody();
    }

    private bool IsValidTarget(Collider other)
    {
        return other.gameObject != null && other.gameObject == combatInfo.Target.gameObject;
    }

    private void ApplyDamage(Collider targetCollider)
    {
        var character = targetCollider.GetComponent<Character>();
        if (character != null)
        {
            character.Damage(damage);
        }
    }

    private bool IsReadyToShoot()
    {
        return user != null && target != null && speed > 0 && damage != 0;
    }

    private void CalculateDirection()
    {
        direction = (target.position - transform.position).normalized;
    }

    private void AddForceToRigidbody()
    {
        rb.AddForce(direction * speed, ForceMode.Impulse);
    }
}

