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
        public ProjectileBase GetBullet(Transform spawnPosition)
        {
            // BulletManager으로부터 bullet Type에 맞는 투사체 생성 및 받아오기
            var bullet = BulletManager.instance.GetBulletFromTransform(properties.Type, spawnPosition);

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
        
        // 투사체의 타입에 따라 설정값을 부여
        if (skillProperties.ProjectileType == ProjectileType.Breakable)
        {
            // 임시로 o번째 특수능력의 지속시간만 활용 
            bulletSettings.properties.Duration = skillProperties.Ability[0].Duration;
        }
       
        // 탄환을 설정에서 생성하고 탄환 정보를 전달
        var bullet = bulletSettings.GetBullet(transform);
        
        bullet.Initialize(bulletSettings.properties);
        bullet.SetTarget(skillProperties.Targets[0]);
        bullet.Shoot();
    }

    public void MovementAction()
    {
        Vector3 destination;
        float reach = skillProperties.Reach;

        // 목표 위치 설정
        switch (skillProperties.TargetMovementLocaction)
        {
            case TargetMovementLocaction.ToEnemy:
                destination = skillProperties.BulletProperties.Target.position;  // 타겟 방향
                break;
            case TargetMovementLocaction.OppositeToEnemy:
                destination = GetOppositeDirection(skillProperties.BulletProperties.Target, reach);  // 타겟 반대 방향
                break;
            case TargetMovementLocaction.Random:
                destination = GetRandomPosition(transform.position, reach);  // 임의의 위치 (범위 10 예시)
                break;
            default:
                Debug.LogWarning("Unsupported movement location type!");
                return;
        }

        // 대시 또는 순간 이동을 수행
        switch (skillProperties.MovementActionType)
        {
            case MovementActionType.Dash:
                DashToTargetLocation(destination, reach);
                break;
            case MovementActionType.Teleport:
                TeleportToTargetLocation(destination);
                break;
            default:
                Debug.LogWarning("Unsupported movement action type!");
                break;
        }
    }

    #region MovementAction

    private void DashToTargetLocation(Vector3 targetLocation, float reach)
    {
        Vector3 dashDirection = (targetLocation - transform.position).normalized;

        // NavMeshAgent를 통해 대시
        gameObject.transform.parent.GetComponent<NavMeshAgent>().Move(dashDirection * reach);
    }

    private void TeleportToTargetLocation(Vector3 targetLocation)
    {
        // 목표 위치로 순간 이동
        transform.position = targetLocation;
    }

    private Vector3 GetOppositeDirection(Transform target, float reach)
    {
        // 타겟의 반대 방향 계산
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Vector3 oppositeDirection = -directionToTarget;  // 반대 방향으로 설정

        return transform.position + oppositeDirection * reach;  // 반대 방향으로 이동할 위치 반환
    }

    private Vector3 GetRandomPosition(Vector3 center, float reach)
    {
        // Center를 중심으로 Range 범위 내에서 랜덤한 위치 반환
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
