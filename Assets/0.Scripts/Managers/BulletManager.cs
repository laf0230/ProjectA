using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public List<GameObject> bulletType = new List<GameObject>();

    public static BulletManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy any additional instance to enforce Singleton
        }
    }

    public Bullet CreateBulletFromTransform(int bulletIndex, Transform transform)
    {
        var bullet = CreateBullet(bulletIndex);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        Debug.Log(bullet);
        return bullet;
    }

    public Bullet CreateBullet(int bulletIndex)
    {
        var bullet = Instantiate(bulletType[bulletIndex]).GetComponent<Bullet>();

        return bullet;
    }
}

