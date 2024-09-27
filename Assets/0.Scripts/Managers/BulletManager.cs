using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public List<GameObject> bulletType = new List<GameObject>();
    public List<Bullet> pool = new List<Bullet>(); // Bullet 타입으로 수정
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
            Bullet bullet = Instantiate(bullet_).GetComponent<Bullet>();
            bullet.gameObject.SetActive(false); // 비활성화
            pool.Add(bullet);
        }
    }

    public Bullet GetBulletFromTransform(int bulletIndex, Transform transform)
    {
        Bullet bullet = GetBullet(bulletIndex);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.gameObject.SetActive(true); // 활성화
        return bullet;
    }

    public Bullet GetBullet(int bulletIndex)
    {
        // 풀에서 사용 가능한 총알 검색
        foreach (var bullet in pool)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                return bullet; // 비활성화된 총알 반환
            }
        }

        if (bulletType[bulletIndex] == null)
        {
            Debug.Log("Bullet이 존재하지 않습니다.");
        }

        // 비활성화된 총알이 없으면 새로 생성
        Bullet newBullet = Instantiate(bulletType[bulletIndex]).GetComponent<Bullet>();


        pool.Add(newBullet); // 풀에 추가
        return newBullet;
    }
}

