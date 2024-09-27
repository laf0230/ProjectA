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
        public BulletProperties properties;
        public Transform User;
        public Transform Target;

        public float speed = 5f;          // źȯ�� �ӵ�
        public float damage = 10f;        // źȯ�� �� ������
        public float reach = 10f;

        // źȯ�� �����ϰ� �ӵ� �� �������� ����
        public Bullet InstantiateBullet(Transform spawnPosition)
        {
            // IsPiercing�� true�� 0, false�� 2�� ���ϰ�, IsSingle�� true�� 0, false�� 1�� ����
            int index = (properties.IsPiercing ? 0 : 2) + (properties.IsSingle ? 0 : 1) + 1;

            // GetBulletFromTransform ȣ��
            var bullet = BulletManager.instance.GetBulletFromTransform(index, spawnPosition);

            bullet.SetProperties(properties);

            return bullet;
        }

        // źȯ�� �� ����
        public void Initialize(BulletProperties bulletInfo)
        {
            damage = bulletInfo.Damage;
            speed = bulletInfo.Speed;
        }

        public void UpdateProperties(BulletProperties properties)
        {
            this.properties = properties;
        }
    }

    // �ν��Ͻ� ����
    public Cooldown cooldown = new Cooldown();       // ��ų ��Ÿ���� �����ϴ� �ν��Ͻ�
    public BulletSettings bulletSettings = new BulletSettings();  // źȯ ���� �ν����Ϳ� ����
    public SkillInfo skillInfo;                    // ��ų ����(źȯ ����)
    public SkillProperties skillProperties;
    [SerializeField] private BulletProperties bulletInfo;  // �ν����Ϳ� ����Ǵ� źȯ ������
    public List<Ability> abilities = new List<Ability>();


    #region Getter/Setter

    // ��ų�̳� źȯ�� ��ǥ�� ����
    public void SetTarget(Transform target)
    {
        bulletInfo.SetTarget(target);
    }

    // ��ų�� ����� �� �ִ��� ���θ� ��ȯ (��Ÿ�� ���� �ƴ�)
    public bool IsUseable() => cooldown.isUseable;

    // ��ų�� ���� ��Ÿ�� ������ ���θ� ��ȯ
    public bool IsCooling() => cooldown.isCooling;

    // ��� �ʿ��� ������ �ʱ�ȭ�ϴ� �޼���
    public void SetSkillInfo(SkillInfo skillInfo)
    {
        Initialize(skillInfo);
    }

    #endregion

    public void Initialize(SkillInfo skillInfo)
    {
        // ��ų ���� �� źȯ ���� �ʱ�ȭ
        this.skillInfo = skillInfo;
        bulletInfo = skillInfo.bulletInfo;

        // ��Ÿ�� �� źȯ ���� ����
        cooldown.SetTotalCoolTime(skillInfo.totalCoolTime); // ��: ��ų�� �浹 �ð����� ��Ÿ�� ����
        bulletSettings.Initialize(bulletInfo);

        // ����, �����, �������� �ɷ��� ���� ��� �߰�
        if (skillInfo.abilityInfos != null)
        {
            abilities = AbilityManager.Instance.GetAbilities(skillInfo.abilityInfos, this);

            foreach (var ability in abilities)
            {
                Debug.Log($"Ability: {ability.Info.Name} has added.");
            }
        }
    }

    public void Initialize(SkillProperties properties)
    {
        // ��ų ���� �� źȯ ���� �ʱ�ȭ
        this.skillProperties = properties;
        bulletInfo = skillInfo.bulletInfo;

        // ��Ÿ�� �� źȯ ���� ����
        cooldown.SetTotalCoolTime(skillInfo.totalCoolTime); // ��: ��ų�� �浹 �ð����� ��Ÿ�� ����
        bulletSettings.Initialize(bulletInfo);

        // ����, �����, �������� �ɷ��� ���� ��� �߰�
        if (skillInfo.abilityInfos != null)
        {
            abilities = AbilityManager.Instance.GetAbilities(skillInfo.abilityInfos, this);

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
        Gizmo();
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

        // źȯ�� �������� �����ϰ� źȯ ������ ����
        var bullet = bulletSettings.InstantiateBullet(transform);
        bullet.Initialize(bulletInfo);
        bullet.Shoot();
    }

    public void MovementAction(MovementActionType type)
    {
        switch (type)
        {
            case MovementActionType.Dash:
                DashToTarget(bulletInfo.Targets[0]);
                break;
            case MovementActionType.Teleport:
                TeleportToTarget(bulletInfo.Targets[0]);
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

    public void Gizmo()
    {
        Vector3 origin = Vector3.zero;
        Vector3 direction = Vector3.up; Ray ray = new Ray(origin, direction);
        float radius = 5f;
        RaycastHit[] hits = Physics.SphereCastAll(ray, radius);
        foreach (var hit in hits)
        {
            // Debug.Log(hit.collider.gameObject.name);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
