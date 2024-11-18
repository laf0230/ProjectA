using UnityEngine;

public class Shooter : MonoBehaviour
{
    Transform target;
    Combat skill;
    public GameObject bullet;

    private void Start()
    {
        skill = gameObject.AddComponent<Combat>();
    }

    private void Update()
    {
        if (target == null || !target.gameObject.activeSelf)
            target = GameObject.FindWithTag("Character").transform;

        if(!skill.IsCooling() && skill.IsUseable())
        {
            skill.gameObject.SetActive(true);
/*
            skill.bulletSettings.Initialize(
                new BulletProperties(
                    ProjectileType.Normal,
                    10,
                    5,
                    transform,
                    10f,
                    false,
                    0
                    )
                );
*/
            // skill.SetTarget( target );
            skill.Use();
        }
    }
}