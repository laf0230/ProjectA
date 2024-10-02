using DG.Tweening;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.AI;

public class Combat : MonoBehaviour
{
    // ��ų ��Ÿ���� �����ϴ� Ŭ����
    [System.Serializable]
    public class Cooldown
    {
        public float totalCoolTime = 10f;   // ��ų�� �� ��Ÿ�� �ð�
        public float currentCoolTime;       // ���� ���� ��Ÿ�� �ð�
        public bool isUseable = true;       // ��ų�� ����� �� �ִ��� ����
        public bool isCooling = false;      // ��ų�� ��Ÿ�� ������ ����

        // ��Ÿ���� �ð��� ���� ���ҽ�Ű�� ��ų�� ��� ������������ Ȯ��
        public void UpdateCooldown()
        {
            if (currentCoolTime > 0)
            {
                currentCoolTime -= Time.deltaTime;
                isUseable = false;
                isCooling = true;
            }
            else
            {
                isUseable = true;  // ��Ÿ���� ������ ��ų�� ����� �� �ְ� ��
                isCooling = false;
            }
        }

        // ��ų ��� �� ��Ÿ���� �ʱ�ȭ
        public void ResetCooldown()
        {
            currentCoolTime = totalCoolTime;
            isUseable = false;   // ��ų ��� �Ұ�
            isCooling = true;
        }

        // ��ų�� �� ��Ÿ���� ����
        public void SetTotalCoolTime(float coolTime)
        {
            totalCoolTime = coolTime;
            currentCoolTime = 0f; // ���� ���� �� ��Ÿ���� ���� ���·� ����
            isUseable = true;
            isCooling = false;
        }
    }

    // źȯ ������ �����ϴ� Ŭ����
    [System.Serializable]
    public class BulletSettings
    {
        [SerializeField] public BulletProperties properties { get; set; }

        // źȯ�� �����ϰ� �ӵ� �� �������� ����
        public Bullet GetBullet(Transform spawnPosition)
        {
            // GetBulletFromTransform ȣ��
            var bullet = BulletManager.instance.GetBulletFromTransform(properties.Type, spawnPosition);

            bullet.SetProperties(properties);

            return bullet;
        }

        // źȯ�� �� ����
        public void Initialize(BulletProperties bulletInfo)
        {
            properties = new BulletProperties(
                bulletInfo.Type,
                bulletInfo.Damage, 
                bulletInfo.Speed, 
                bulletInfo.User, 
                bulletInfo.Reach);
        }

        public void UpdateProperties(BulletProperties properties)
        {
            this.properties = properties;
        }
    }

    // �ν��Ͻ� ����
    public Cooldown cooldown = new Cooldown();       // ��ų ��Ÿ���� �����ϴ� �ν��Ͻ�
    public BulletSettings bulletSettings = new BulletSettings();  // źȯ ���� �ν����Ϳ� ����
    [field: SerializeField] public SkillProperties skillProperties;
    public List<Ability> abilities = new List<Ability>();

    #region Getter/Setter

    // ��ų�̳� źȯ�� ��ǥ�� ����
    public void SetTarget(List<Transform> targets)
    {
        skillProperties.Targets = targets;
        bulletSettings.properties.SetTarget(targets[0]);
    }

    // ��ų�� ����� �� �ִ��� ���θ� ��ȯ (��Ÿ�� ���� �ƴ�)
    public bool IsUseable() => cooldown.isUseable;

    // ��ų�� ���� ��Ÿ�� ������ ���θ� ��ȯ
    public bool IsCooling() => cooldown.isCooling;

    // ��� �ʿ��� ������ �ʱ�ȭ�ϴ� �޼���
    public void SetSkillInfo(SkillProperties skillInfo)
    {
        Initialize(skillInfo);
    }

    #endregion

    public void Initialize(SkillProperties properties)
    {
        /*
        skillProperties = new SkillProperties(
            properties.user,
            properties.Type,
            properties.ShapeType,
            properties.TargetType,
            properties.CoolTime,
            properties.BulletType,
            properties.Damage,
            properties.Speed,
            properties.Reach,
            properties.Ability
            );
        */
        // ��ų ���� �� źȯ ���� �ʱ�ȭ
        this.skillProperties = properties;
        bulletSettings.properties = skillProperties.BulletProperties;

        // ��Ÿ�� �� źȯ ���� ����
        cooldown.SetTotalCoolTime(skillProperties.CoolTime); // ��: ��ų�� �浹 �ð����� ��Ÿ�� ����
        bulletSettings.Initialize(skillProperties.BulletProperties);

        // ����, �����, �������� �ɷ��� ���� ��� �߰�
        if (skillProperties.Ability != null)
        {
            abilities = AbilityManager.Instance.GetAbilities(skillProperties.Ability, this);

            foreach (var ability in abilities)
            {
                Debug.Log($"Ability: {ability.Info.Name} has added.");
            }
        }
    }

    // �� �����Ӹ��� ȣ��Ǵ� Update �޼���
    private void Update()
    {
        // ���� ���� ���� ��Ÿ���� ���������� ���ҽ�Ŵ
        cooldown.UpdateCooldown();
    }


    // ��ų�� ����ϴ� �޼���
    public void Use()
    {
        // ��ų�� ��� �������� Ȯ�� (��Ÿ�� ���̸� ��� �Ұ�)
        if (!cooldown.isUseable)
        {
            Debug.Log("Skill is on cooldown");
            return;
        }

        skillProperties.BulletProperties.SetTarget(skillProperties.Targets[0]);
        // źȯ�� �������� �����ϰ� źȯ ������ ����
        var bullet = bulletSettings.GetBullet(transform);
        bullet.Initialize(bulletSettings.properties);
        bullet.Shoot();
    }

    public void MovementAction(MovementActionType MAType, TargetMovementLocaction TMLType)
    {
        switch (MAType)
        {
            case MovementActionType.Dash:
                switch (TMLType)
                {
                    case TargetMovementLocaction.ToEnemy:
                        break;
                    case TargetMovementLocaction.OppositeToEnemy:
                        break;
                    case TargetMovementLocaction.Random:
                        break;
                    default:
                        break;
                }
                DashToTarget(skillProperties.BulletProperties.Target);
                break;
            case MovementActionType.Teleport:
                TeleportToTarget(skillProperties.BulletProperties.Target);
                break;
                // �ٸ� MovementActionType�� ���� ���̽� �߰� ����
        }
    }
    private void DashToTarget(Transform target)
    {
        // ����� �Ÿ� ���� (��: 5.0f)
        float dashDistance = 5.0f;
        Vector3 dashDirection = (target.position - transform.position).normalized;
        Vector3 dashPosition = transform.position + dashDirection * dashDistance;

        // NavMeshAgent�� ���� ���
        gameObject.transform.parent.GetComponent<NavMeshAgent>().Move(dashDirection * dashDistance);
    }

    private void TeleportToTarget(Transform target)
    {
        // ��ǥ ��ġ�� ���� �̵�
        transform.position = target.position;
    }

    private Vector3 GetRandomPosition(Vector3 center, float range)
    {
        // Center�� �߽����� Range�� ���� ���� �������� Ư���� ������ ��ȯ�ϴ� �Լ�
        // Generate random angles
        float randomAngle1 = Random.Range(0f, Mathf.PI * 2); // Azimuthal angle
        float randomAngle2 = Random.Range(0f, Mathf.PI); // Polar angle

        // Calculate spherical coordinates
        float x = range * Mathf.Sin(randomAngle2) * Mathf.Cos(randomAngle1);
        float y = range * Mathf.Sin(randomAngle2) * Mathf.Sin(randomAngle1);
        float z = range * Mathf.Cos(randomAngle2);

        // Return the random position offset from the center
        return center + new Vector3(x, y, z);
    }

    public List<Collider> GetEnemies()
    {
        Vector3 origin = Vector3.zero;
        Vector3 direction = Vector3.up; Ray ray = new Ray(origin, direction);
        List<Collider> list = new List<Collider>();
        float radius = 5f;
        RaycastHit[] hits = Physics.SphereCastAll(ray, radius);
        foreach (var hit in hits)
        {
            list.Add(hit.collider);
        }
        return list;
    }

    public void OnDrawGizmos()
    {
        switch (skillProperties.Type)
        {
            case SkillType.Attack:
                Gizmos.color = Color.green;
                break;
                case SkillType.Skill:
                Gizmos.color = Color.yellow;
                break;
            case SkillType.Ultimate:
                Gizmos.color = Color.red;
                break;
        }
        Gizmos.DrawWireSphere(transform.position, skillProperties.Reach);
    }
}
