using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    // 스킬 쿨타임을 관리하는 클래스
    [System.Serializable]
    public class Cooldown
    {
        public float totalCoolTime = 10f;   // 스킬의 총 쿨타임 시간
        public float currentCoolTime;       // 현재 남은 쿨타임 시간
        public bool isUseable = true;       // 스킬을 사용할 수 있는지 여부
        public bool isCooling = false;      // 스킬이 쿨타임 중인지 여부

        // 쿨타임을 시간에 따라 감소시키고 스킬이 사용 가능해졌는지 확인
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
                isUseable = true;  // 쿨타임이 끝나면 스킬을 사용할 수 있게 됨
                isCooling = false;
            }
        }

        // 스킬 사용 후 쿨타임을 초기화
        public void ResetCooldown()
        {
            currentCoolTime = totalCoolTime;
            isUseable = false;   // 스킬 사용 불가
            isCooling = true;
        }

        // 스킬의 총 쿨타임을 설정
        public void SetTotalCoolTime(float coolTime)
        {
            totalCoolTime = coolTime;
            currentCoolTime = 0f; // 최초 설정 시 쿨타임이 없는 상태로 시작
            isUseable = true;
            isCooling = false;
        }
    }

    // 탄환 설정을 관리하는 클래스
    [System.Serializable]
    public class BulletSettings
    {
        public float speed = 5f;          // 탄환의 속도
        public float damage = 10f;        // 탄환이 줄 데미지
        public float reach = 10f;

        // 탄환을 생성하고 속도 및 데미지를 설정
        public Bullet InstantiateBullet(Transform spawnPosition)
        {
            var bullet = BulletManager.instance.GetBulletFromTransform(0, spawnPosition);
            bullet.SetSpeed(speed);
            bullet.SetDamage(damage);
            bullet.SetReach(reach);
            return bullet;
        }
        
        // 탄환의 값 설정
        public void Initialize(BulletInfo bulletInfo)
        {
            damage = bulletInfo.Damage;
            speed = bulletInfo.Speed;
        }
    }

    // 인스턴스 변수
    public Cooldown cooldown = new Cooldown();       // 스킬 쿨타임을 관리하는 인스턴스
    public BulletSettings bulletSettings = new BulletSettings();  // 탄환 설정 인스펙터에 노출
    public SkillInfo skillInfo;                    // 스킬 정보(탄환 포함)
    [SerializeField] private BulletInfo bulletInfo;  // 인스펙터에 노출되는 탄환 데이터
    public List<Ability_> abilities = new List<Ability_>();

    // 매 프레임마다 호출되는 Update 메서드
    private void Update()
    {
        // 게임 루프 동안 쿨타임을 지속적으로 감소시킴
        cooldown.UpdateCooldown();
    }

    // 모든 필요한 정보를 초기화하는 메서드
    public void Initialize(SkillInfo skillInfo)
    {
        // 스킬 정보 및 탄환 정보 초기화
        this.skillInfo = skillInfo;
        bulletInfo = skillInfo.bulletInfo;

        // 쿨타임 및 탄환 정보 설정
        cooldown.SetTotalCoolTime(skillInfo.totalCoolTime); // 예: 스킬의 충돌 시간으로 쿨타임 설정
        bulletSettings.Initialize(bulletInfo);

        // 버프, 디버프, 군중제어 능력이 있을 경우 추가
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

    // 스킬을 사용하는 메서드
    public void Use()
    {
        // 스킬이 사용 가능한지 확인 (쿨타임 중이면 사용 불가)
        if (!cooldown.isUseable)
        {
            Debug.Log("Skill is on cooldown");
            return;
        }

        // 탄환을 설정에서 생성하고 탄환 정보를 전달
        var bullet = bulletSettings.InstantiateBullet(transform);
        bullet.Initialize(bulletInfo);
        bullet.Shoot();
    }

    // 스킬이나 탄환의 목표를 설정
    public void SetTarget(Transform target)
    {
        bulletInfo.SetTarget(target);
    }

    // 스킬을 사용할 수 있는지 여부를 반환 (쿨타임 중이 아님)
    public bool IsUseable() => cooldown.isUseable;

    // 스킬이 현재 쿨타임 중인지 여부를 반환
    public bool IsCooling() => cooldown.isCooling;
}

