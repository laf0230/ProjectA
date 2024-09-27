using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public List<GameObject> bulletType = new List<GameObject>();
    public List<Bullet> pool = new List<Bullet>(); // Bullet Ÿ������ ����
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
            bullet.gameObject.SetActive(false); // ��Ȱ��ȭ
            pool.Add(bullet);
        }
    }

    public Bullet GetBulletFromTransform(int bulletIndex, Transform transform)
    {
        Bullet bullet = GetBullet(bulletIndex);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.gameObject.SetActive(true); // Ȱ��ȭ
        return bullet;
    }

    public Bullet GetBullet(int bulletIndex)
    {
        // Ǯ���� ��� ������ �Ѿ� �˻�
        foreach (var bullet in pool)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                return bullet; // ��Ȱ��ȭ�� �Ѿ� ��ȯ
            }
        }

        if (bulletType[bulletIndex] == null)
        {
            Debug.Log("Bullet�� �������� �ʽ��ϴ�.");
        }

        // ��Ȱ��ȭ�� �Ѿ��� ������ ���� ����
        Bullet newBullet = Instantiate(bulletType[bulletIndex]).GetComponent<Bullet>();


        pool.Add(newBullet); // Ǯ�� �߰�
        return newBullet;
    }
}

