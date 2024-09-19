using Unity.VisualScripting;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    Transform target;
    Combat skill;
    public GameObject bullet;

    private void Start()
    {
        skill = gameObject.GetOrAddComponent<Combat>();
        skill.bulletSettings.bulletPrefab = bullet;
    }

    private void Update()
    {
        if (target == null || !target.gameObject.activeSelf)
            target = GameObject.FindWithTag("Character").transform;

        if(!skill.IsCooling() && skill.IsUseable())
        {
            skill.gameObject.SetActive(true);

            skill.bulletSettings.Initialize(
                new BulletInfo(
                    10,
                    5,
                    transform
                    )
                );
            skill.SetTarget( target );
            skill.Use();
        }
    }
}