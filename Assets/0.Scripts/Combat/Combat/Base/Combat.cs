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
        [SerializeField] public BulletProperties properties { get; set; } = new BulletProperties();

        // źȯ�� �����ϰ� �ӵ� �� �������� ����
        public ProjectileBase GetBullet(Transform spawnPosition)
        {
            if(spawnPosition == null)
            {
                Debug.LogWarning("bullet�� ������ ����Ʈ ���� null���Դϴ�.");
            }

            // BulletManager���κ��� bullet Type�� �´� ����ü ���� �� �޾ƿ���
            var bullet = BulletManager.instance.GetBulletFromTransform(properties.Type, spawnPosition);

            if(bullet == null)
            {
                Debug.LogError($"{bullet.name}�� null�� ��ȯ�߽��ϴ�");
            }

            return bullet;
        }

        // źȯ�� �� ����
        public void Initialize(BulletProperties bulletInfo)
        {
            properties.Initilize(
                bulletInfo.Type,
                bulletInfo.Damage, 
                bulletInfo.Speed, 
                bulletInfo.User, 
                bulletInfo.Reach,
                bulletInfo.isUsedOwnPlace,
                bulletInfo.Duration
                );
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

        // ����ü�� Ÿ�Կ� ���� �������� �ο�
        if (skillProperties.ProjectileType == ProjectileType.Breakable)
        {
            // �ӽ÷� o��° Ư���ɷ��� ���ӽð��� Ȱ�� 
            bulletSettings.properties.Duration = skillProperties.duration;
            Debug.Log("�극��Ŀ�� ����ü");
        }

        if (bulletSettings.properties.Type == ProjectileType.None) 
            return;
            // ����ü�� ������� �ʰ� ������ �� ��� ����
            // ����ü�� ���� ��������, �ڱ� ���� ȿ�� ��

        // źȯ�� �������� �����ϰ� źȯ ������ ����
        var bullet = bulletSettings.GetBullet(transform);
        if(bullet == null)
        {
            Debug.LogWarning("Bullet returned null");
        }

        bullet.Initialize(bulletSettings.properties);
        bullet.SetTarget(skillProperties.Targets[0]);
        Debug.Log(bullet.Properties.Type + " �Ҹ� Ÿ��");
        bullet.Shoot();
    }

    // �̵��� �ڵ�
    public void MovementAction()
    {
        float reach = skillProperties.Reach;


        // ��� �Ǵ� ���� �̵��� ����
        switch (skillProperties.MovementActionType)
        {
            case MovementActionType.Dash:
                DashToTargetLocation(
                    GetTargetDirection(
                        skillProperties.TargetMovementLocaction, reach
                        ), reach);
                break;
            case MovementActionType.Teleport:
                TeleportToTargetLocation(
                    GetTargetDirection(
                        skillProperties.TargetMovementLocaction, reach
                        ));
                break;
            default:
                Debug.LogWarning("Unsupported movement action type!");
                break;
        }
    }

    public Vector3 GetTargetDirection(TargetMovementLocaction destinationType, float reach)
    {
        // ��ǥ ��ġ ����
        switch (destinationType)
        {
            case TargetMovementLocaction.ToEnemy:
                return skillProperties.BulletProperties.Target.position;  // Ÿ�� ����
            case TargetMovementLocaction.OppositeToEnemy:
                return GetOppositeDirection(skillProperties.BulletProperties.Target, reach);  // Ÿ�� �ݴ� ����
            case TargetMovementLocaction.Random:
                return GetRandomPosition(transform.position, reach);  // ������ ��ġ (���� 10 ����)
            default:
                Debug.LogWarning("Unsupported movement location type!");
                return Vector3.zero;
        }
    }

    #region MovementAction

    private void DashToTargetLocation(Vector3 targetLocation, float reach)
    {
        Vector3 dashDirection = (targetLocation - transform.position).normalized;

        // NavMeshAgent�� ���� ���
        gameObject.transform.parent.GetComponent<NavMeshAgent>().Move(dashDirection * reach);
    }

    private void TeleportToTargetLocation(Vector3 targetLocation)
    {
        // ��ǥ ��ġ�� ���� �̵�
        transform.position = targetLocation;
    }

    private Vector3 GetOppositeDirection(Transform target, float reach)
    {
        // Ÿ���� �ݴ� ���� ���
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Vector3 oppositeDirection = -directionToTarget;  // �ݴ� �������� ����

        return transform.position + oppositeDirection * reach;  // �ݴ� �������� �̵��� ��ġ ��ȯ
    }

    private Vector3 GetRandomPosition(Vector3 center, float reach)
    {
        // Center�� �߽����� Range ���� ������ ������ ��ġ ��ȯ
        return center + Random.insideUnitSphere * reach;
    }

    #endregion

    public List<Collider> GetEnemies()
    {
        Vector3 origin = Vector3.zero;
        Vector3 direction = Vector3.up; Ray ray = new Ray(origin, direction);
        List<Collider> list = new List<Collider>();
        float radius = skillProperties.Reach;
        RaycastHit[] hits = Physics.SphereCastAll(ray, radius);
        foreach (var hit in hits)
        {
            list.Add(hit.collider);
        }
        return list;
    }
    #region Debug


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

    #endregion
}
