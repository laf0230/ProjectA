using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BulletProperties Properties { get; set; }
    public Vector3 direction;
    float distance;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // reach 만큼의 거리이 지나면 사라지는 코드
        distance = Vector3.Distance(Properties.User.position, transform.position);

        if (distance > Properties.Reach + 1.5f)
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

    public void Initialize(BulletProperties properties)
    {
        this.Properties = properties;
    }

    public void SetProperties(BulletProperties properties)
    {
        this.Properties = properties;
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
        return other.gameObject != null && 
            other.gameObject == Properties.Target.gameObject && 
            other.gameObject != !Properties.User.gameObject;
    }

    private void ApplyDamage(Collider targetCollider)
    {
        var character = targetCollider.GetComponent<Character>();
        if (character != null)
        {
            character.Damage(Properties.Damage);
        }
    }

    private bool IsReadyToShoot()
    {
        return Properties.User != null && Properties.Target != null && Properties.Speed > 0 && Properties.Damage != 0;
    }

    private void CalculateDirection()
    {
        direction = (Properties.Target.position - transform.position).normalized;
    }

    private void AddForceToRigidbody()
    {
        rb.AddForce(direction * Properties.Speed, ForceMode.Impulse);
    }
}

