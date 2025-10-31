using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionProjectile : ProjectileBase
{
    public float explosionRadius = 5.0f;
    public float groundHeight = 0.1f;
    public float fieldDuration; // �ʵ� �����ð�
    public int areaDamage = 10;
    public GameObject GroundEffect;
    public GameObject Bullet;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        // �ε�ģ ����� Ÿ������ ���Ǻ� Ȯ��
        if ((other.gameObject == Properties.Target.gameObject) || Properties.isUsedOwnPlace)
        {
            Debug.Log(Properties.isUsedOwnPlace ? "���ڸ��� ���� ��ġ" : "����ڸ�: " +  Properties.User + " ���ڸ��� ��ġ���� ���� �����Դϴ�.");
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
            // ���� Ȱ��ȭ
        GroundEffect.SetActive(isActive);

        if (isActive)
        {
            var groundEffect = GroundEffect.GetComponent<FieldEffect>();
            groundEffect.Initilize(Properties);
            Vector3 groundPosition = new Vector3(transform.position.x, groundHeight, transform.position.z);
            GroundEffect.transform.position = groundPosition;
            rb.linearVelocity = Vector3.zero;
        
            StartCoroutine("IActiveField");
        }
    }

    public IEnumerator IActiveField()
    {
        fieldDuration = Properties.Duration;

        while (fieldDuration > 0)
        {
            fieldDuration -= Time.deltaTime;
            yield return null;
        }

        ActiveFieldEffect(false);
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
    }
}
