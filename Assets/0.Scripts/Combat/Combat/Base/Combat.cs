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
        [SerializeField] public BulletProperties properties { get; set; }

        // 탄환을 생성하고 속도 및 데미지를 설정
        public Bullet GetBullet(Transform spawnPosition)
        {
            // GetBulletFromTransform 호출
            var bullet = BulletManager.instance.GetBulletFromTransform(properties.Type, spawnPosition);

            bullet.SetProperties(properties);

            return bullet;
        }

        // 탄환의 값 설정
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

    // 인스턴스 변수
    public Cooldown cooldown = new Cooldown();       // 스킬 쿨타임을 관리하는 인스턴스
    public BulletSettings bulletSettings = new BulletSettings();  // 탄환 설정 인스펙터에 노출
    [field: SerializeField] public SkillProperties skillProperties;
    public List<Ability> abilities = new List<Ability>();

    #region Getter/Setter

    // 스킬이나 탄환의 목표를 설정
    public void SetTarget(List<Transform> targets)
    {
        skillProperties.Targets = targets;
        bulletSettings.properties.SetTarget(targets[0]);
    }

    // 스킬을 사용할 수 있는지 여부를 반환 (쿨타임 중이 아님)
    public bool IsUseable() => cooldown.isUseable;

    // 스킬이 현재 쿨타임 중인지 여부를 반환
    public bool IsCooling() => cooldown.isCooling;

    // 모든 필요한 정보를 초기화하는 메서드
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
        // 스킬 정보 및 탄환 정보 초기화
        this.skillProperties = properties;
        bulletSettings.properties = skillProperties.BulletProperties;

        // 쿨타임 및 탄환 정보 설정
        cooldown.SetTotalCoolTime(skillProperties.CoolTime); // 예: 스킬의 충돌 시간으로 쿨타임 설정
        bulletSettings.Initialize(skillProperties.BulletProperties);

        // 버프, 디버프, 군중제어 능력이 있을 경우 추가
        if (skillProperties.Ability != null)
        {
            abilities = AbilityManager.Instance.GetAbilities(skillProperties.Ability, this);

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

        skillProperties.BulletProperties.SetTarget(skillProperties.Targets[0]);
        // 탄환을 설정에서 생성하고 탄환 정보를 전달
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

    private Vector3 GetRandomPosition(Vector3 center, float range)
    {
        // Center를 중심으로 Range의 범위 내의 공간에서 특정한 지점을 반환하는 함수
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
