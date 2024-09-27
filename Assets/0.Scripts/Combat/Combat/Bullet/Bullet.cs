using UnityEngine;

public class Bullet : Poolable
{
    private BulletProperties combatInfo;
    private Transform user;
    private Transform target;
    public float speed;
    public float damage;
    public float reach;
    public bool isPiercing = false;
    public Vector3 direction;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // reach 만큼의 시간이 지나면 사라지는 코드
        reach -= Time.deltaTime;
        if (reach < 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 목표로 하는 타깃일 경우 데미지 부여
        if (IsValidTarget(other))
        {
            ApplyDamage(other);
            gameObject.SetActive(false);
        }

        // 임시 코드 타깃 관통
        if (other.GetComponent<Character>() != null && false)
        {
            ApplyDamage(other);
            if (IsValidTarget(other))
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void Initialize(BulletProperties combatInfo)
    {
        this.combatInfo = combatInfo;
        SetUser(combatInfo.User);
        SetTarget(combatInfo.Targets[0]);
        SetSpeed(combatInfo.Speed);
        SetDamage(combatInfo.Damage);
        SetReach(combatInfo.Reach);
    }

    public void Intialize(BulletProperties properties)
    {
        SetUser(properties.User);
        SetSpeed(properties.Speed);
        SetDamage(properties.Damage);
        SetReach(properties.Reach);
    }

    public void SetProperties(BulletProperties properties)
    {
        speed = properties.Speed;
        damage = properties.Damage;
        reach = properties.Reach;
    }

    public void SetUser(Transform user)
    {
        this.user = user;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    #region Info
    public void SetCombatInfo(BulletProperties combatInfo)
    {
        this.combatInfo = combatInfo;
    }


    public void SetSpeed(float speed = 10f)
    {
        this.speed = speed;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetReach(float reach)
    {
        this.reach = reach;
    }

    #endregion

    public void Shoot()
    {
        SetReach(combatInfo.Reach);
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
        return other.gameObject != null && other.gameObject == combatInfo.Targets[0].gameObject;
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

