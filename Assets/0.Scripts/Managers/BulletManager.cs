using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    Normal, // ���� �� ������� Ÿ��
    Breakable, // ���� �� ������ ���� ������ �ϴ� Ÿ��
    None
}

public class BulletManager : MonoBehaviour
{
    public List<GameObject> bulletType = new List<GameObject>();
    public List<ProjectileBase> pool = new List<ProjectileBase>(); // Bullet Ÿ������ ����
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
            bullet.gameObject.SetActive(false); // ��Ȱ��ȭ
            pool.Add(bullet);
        }
    }

    public ProjectileBase GetBulletFromTransform(ProjectileType bulletIndex, Transform transform)
    {
        ProjectileBase bullet = GetBullet(bulletIndex);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.gameObject.SetActive(true); // Ȱ��ȭ
        return bullet;
    }

    private ProjectileBase GetBullet(ProjectileType bulletIndex)
    {
        // Ǯ���� ��� ������ �Ѿ� �˻�
        foreach (var bullet in pool)
        {
            if (!bullet.gameObject.activeSelf)
            {
                return bullet; // ��Ȱ��ȭ�� �Ѿ� ��ȯ
            }
        }

        if (bulletType[((int)bulletIndex)] == null)
        {
            Debug.Log("Bullet�� �������� �ʽ��ϴ�.");
        }

        // ��Ȱ��ȭ�� �Ѿ��� ������ ���� ����
        ProjectileBase newBullet = Instantiate(
            GetBulletFromType(bulletIndex)
            ).GetComponent<ProjectileBase>();

        pool.Add(newBullet); // Ǯ�� �߰�
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

