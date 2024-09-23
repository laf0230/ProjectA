using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

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
        public float speed = 5f;          // źȯ�� �ӵ�
        public float damage = 10f;        // źȯ�� �� ������
        public float reach = 10f;

        // źȯ�� �����ϰ� �ӵ� �� �������� ����
        public Bullet InstantiateBullet(Transform spawnPosition)
        {
            var bullet = BulletManager.instance.GetBulletFromTransform(0, spawnPosition);
            bullet.SetSpeed(speed);
            bullet.SetDamage(damage);
            bullet.SetReach(reach);
            return bullet;
        }
        
        // źȯ�� �� ����
        public void Initialize(BulletInfo bulletInfo)
        {
            damage = bulletInfo.Damage;
            speed = bulletInfo.Speed;
        }
    }

    // �ν��Ͻ� ����
    public Cooldown cooldown = new Cooldown();       // ��ų ��Ÿ���� �����ϴ� �ν��Ͻ�
    public BulletSettings bulletSettings = new BulletSettings();  // źȯ ���� �ν����Ϳ� ����
    public SkillInfo skillInfo;                    // ��ų ����(źȯ ����)
    [SerializeField] private BulletInfo bulletInfo;  // �ν����Ϳ� ����Ǵ� źȯ ������
    public List<Ability_> abilities = new List<Ability_>();

    // �� �����Ӹ��� ȣ��Ǵ� Update �޼���
    private void Update()
    {
        // ���� ���� ���� ��Ÿ���� ���������� ���ҽ�Ŵ
        cooldown.UpdateCooldown();
    }

    // ��� �ʿ��� ������ �ʱ�ȭ�ϴ� �޼���
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

    public void SetSkillInfo(SkillInfo skillInfo)
    {
        Initialize(skillInfo);
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

    // ��ų�̳� źȯ�� ��ǥ�� ����
    public void SetTarget(Transform target)
    {
        bulletInfo.SetTarget(target);
    }

    // ��ų�� ����� �� �ִ��� ���θ� ��ȯ (��Ÿ�� ���� �ƴ�)
    public bool IsUseable() => cooldown.isUseable;

    // ��ų�� ���� ��Ÿ�� ������ ���θ� ��ȯ
    public bool IsCooling() => cooldown.isCooling;
}

