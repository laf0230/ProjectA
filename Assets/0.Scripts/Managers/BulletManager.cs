using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    Normal, // 명중 시 사라지는 타입
    Breakable, // 명중 시 깨지며 범위 공격을 하는 타입
    None
}

public class BulletManager : MonoBehaviour
{
    public List<GameObject> bulletType = new List<GameObject>();
    public List<ProjectileBase> pool = new List<ProjectileBase>(); // Bullet 타입으로 수정
    public int poolCount;

    public static BulletManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Singleton enforcement
        }
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        foreach(var bullet_ in bulletType)
        {
            ProjectileBase bullet = Instantiate(bullet_).GetComponent<ProjectileBase>();
            bullet.gameObject.SetActive(false); // 비활성화
            pool.Add(bullet);
        }
    }

    public ProjectileBase GetBulletFromTransform(ProjectileType bulletIndex, Transform transform)
    {
        ProjectileBase bullet = GetBullet(bulletIndex);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.gameObject.SetActive(true); // 활성화
        return bullet;
    }

    private ProjectileBase GetBullet(ProjectileType bulletIndex)
    {
        // 풀에서 사용 가능한 총알 검색
        foreach (var bullet in pool)
        {
            if (!bullet.gameObject.activeSelf)
            {
                return bullet; // 비활성화된 총알 반환
            }
        }

        if (bulletType[((int)bulletIndex)] == null)
        {
            Debug.Log("Bullet이 존재하지 않습니다.");
        }

        // 비활성화된 총알이 없으면 새로 생성
        ProjectileBase newBullet = Instantiate(
            GetBulletFromType(bulletIndex)
            ).GetComponent<ProjectileBase>();

        pool.Add(newBullet); // 풀에 추가
        return newBullet;
    }

    public GameObject GetBulletFromType(ProjectileType type)
    {
        foreach (var item in bulletType)
        {
            var projectile = item.GetComponent<ProjectileBase>();

            if(projectile.type == type)
            {
                return item;
            }
        }
        return null;
    }
}

