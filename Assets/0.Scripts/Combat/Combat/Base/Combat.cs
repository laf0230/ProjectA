using DG.Tweening;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.AI;

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
        public BulletProperties properties;
        public Transform User;
        public Transform Target;

        public float speed = 5f;          // 탄환의 속도
        public float damage = 10f;        // 탄환이 줄 데미지
        public float reach = 10f;

        // 탄환을 생성하고 속도 및 데미지를 설정
        public Bullet InstantiateBullet(Transform spawnPosition)
        {
            // IsPiercing이 true면 0, false면 2를 더하고, IsSingle이 true면 0, false면 1을 더함
            int index = (properties.IsPiercing ? 0 : 2) + (properties.IsSingle ? 0 : 1) + 1;

            // GetBulletFromTransform 호출
            var bullet = BulletManager.instance.GetBulletFromTransform(index, spawnPosition);

            bullet.SetProperties(properties);

            return bullet;
        }

        // 탄환의 값 설정
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

    // 인스턴스 변수
    public Cooldown cooldown = new Cooldown();       // 스킬 쿨타임을 관리하는 인스턴스
    public BulletSettings bulletSettings = new BulletSettings();  // 탄환 설정 인스펙터에 노출
    public SkillInfo skillInfo;                    // 스킬 정보(탄환 포함)
    public SkillProperties skillProperties;
    [SerializeField] private BulletProperties bulletInfo;  // 인스펙터에 노출되는 탄환 데이터
    public List<Ability> abilities = new List<Ability>();


    #region Getter/Setter

    // 스킬이나 탄환의 목표를 설정
    public void SetTarget(Transform target)
    {
        bulletInfo.SetTarget(target);
    }

    // 스킬을 사용할 수 있는지 여부를 반환 (쿨타임 중이 아님)
    public bool IsUseable() => cooldown.isUseable;

    // 스킬이 현재 쿨타임 중인지 여부를 반환
    public bool IsCooling() => cooldown.isCooling;

    // 모든 필요한 정보를 초기화하는 메서드
    public void SetSkillInfo(SkillInfo skillInfo)
    {
        Initialize(skillInfo);
    }

    #endregion

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

    public void Initialize(SkillProperties properties)
    {
        // 스킬 정보 및 탄환 정보 초기화
        this.skillProperties = properties;
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

    // 매 프레임마다 호출되는 Update 메서드
    private void Update()
    {
        // 게임 루프 동안 쿨타임을 지속적으로 감소시킴
        cooldown.UpdateCooldown();
        Gizmo();
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
                // 다른 MovementActionType에 대한 케이스 추가 가능
        }
    }
    private void DashToTarget(Transform target)
    {
        // 대시할 거리 설정 (예: 5.0f)
        float dashDistance = 5.0f;
        Vector3 dashDirection = (target.position - transform.position).normalized;
        Vector3 dashPosition = transform.position + dashDirection * dashDistance;

        // NavMeshAgent를 통해 대시
        gameObject.transform.parent.GetComponent<NavMeshAgent>().Move(dashDirection * dashDistance);
    }

    private void TeleportToTarget(Transform target)
    {
        // 목표 위치로 순간 이동
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
