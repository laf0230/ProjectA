using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionProjectile : ProjectileBase
{
    public float explosionRadius = 5.0f;
    public float groundHeight = 0.1f;
    public float fieldDuration; // 필드 유지시간
    public int areaDamage = 10;
    public GameObject GroundEffect;
    public GameObject Bullet;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        // 부디친 대상이 타겟인지 조건부 확인
        if (other.gameObject == Properties.Target.gameObject)
        {
            Debug.Log("타격 성공");
            OnImpact(other);
        }
    }

    protected override void OnImpact(Collider other)
    {
        if(Bullet == null)
        {
            Debug.LogWarning(Bullet + " Object is Null");
        }
        ActiveProjectile(false);
        ActiveFieldEffect(true);
    }

    public void ActiveProjectile(bool isActive)
    {
        Bullet.SetActive(isActive);
    }

    public void ActiveFieldEffect(bool isActive)
    {
            // 장판 활성화
        GroundEffect.SetActive(isActive);

        if (isActive)
        {
            var groundEffect = GroundEffect.GetComponent<FieldEffect>();
            groundEffect.Initilize(Properties);
            Vector3 groundPosition = new Vector3(transform.position.x, groundHeight, transform.position.z);
            GroundEffect.transform.position = groundPosition;
            rb.velocity = Vector3.zero;
        
            StartCoroutine("IActiveField");
        }
    }

    public IEnumerator IActiveField()
    {
        float startTime = 0;
        float endTime = Properties.Duration;

        while (startTime > endTime)
        {
            startTime += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
    }
}
