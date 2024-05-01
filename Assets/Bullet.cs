using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Character character { get; set; }
    public Character targetCharacter;
    Rigidbody rb;

    public float damage;
    public float speed = 10f;
    public bool IsPenetration { get; set; } // ���� ����
    public bool IsArea { get; set; } // ������ ����
    public bool IsTrakcing { get; set; }

    SkillDataSO skilldata { get; set; }

    private void Start()
    {
        character = skilldata.SelfCharacter;
        targetCharacter = skilldata.Target;
        damage = skilldata.Damage;
        IsPenetration = skilldata.IsPenetration;
        IsArea = skilldata.IsArea;
        IsTrakcing = skilldata.IsTracking;

        rb = GetComponent<Rigidbody>();
    }

    public void InitData(SkillDataSO _skillDataSO)
    {
        skilldata = _skillDataSO;
    }

    public void Shoot()
    {
        Vector3 direction = targetCharacter.transform.position - character.transform.position;

        if (IsTrakcing)
        {
            // ����
            rb.velocity = direction * speed;
        }
        else
        {
            // ������
            rb.AddForce(direction * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != character.gameObject && other.CompareTag("Character"))
        {
            // Ÿ�ٸ� ����
            targetCharacter = other.gameObject.GetComponent<Character>();

            targetCharacter.Damage(damage);
        }
        gameObject.SetActive(false);
    }
}
