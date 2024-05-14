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
    public bool IsPenetration { get; set; } // 관통 여부
    public bool IsArea { get; set; } // 광범위 여부
    public bool IsTrakcing { get; set; }
    public float duration;
    public float totalDuration;

    SkillDataSO skilldata { get; set; }

    private void Start()
    {
        targetCharacter = skilldata.Target;
        damage = skilldata.Damage;
        IsPenetration = skilldata.IsPenetration;
        IsArea = skilldata.IsArea;
        IsTrakcing = skilldata.IsTracking;
        totalDuration = skilldata.Duration;

        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
    }

    private void Update()
    {
        if (duration > 0f)
        {
            duration -= Time.deltaTime;
        }
        else if (duration <= 0)
        {
            InitDuration();
            gameObject.SetActive(false);
        }
    }

    public void InitDuration()
    {
        duration = skilldata.Duration;
    }

    public void InitData(SkillDataSO _skillDataSO, Character character)
    {
        skilldata = _skillDataSO;
        this.character = character;
    }

    public void Shoot(Vector3 targetPosition)
    {
        // Left and Right 

        if (IsTrakcing)
        {
            // 추적
            rb.velocity = targetPosition - transform.position * speed * Time.deltaTime;
        }
        else
        {
            // 날리기
            rb.AddForce(targetPosition * speed, ForceMode.Acceleration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != character.gameObject && other.CompareTag("Character"))
        {
            Debug.Log(other.gameObject);
            // 타겟만 공격
            targetCharacter = other.gameObject.GetComponent<Character>();

            targetCharacter.Damage(damage);
        }
        gameObject.SetActive(false);
    }
}
