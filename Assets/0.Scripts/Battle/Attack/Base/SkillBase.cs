using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour, SkillState
{
    [field: SerializeField] public Character Character { get; set; }
    public Character Target { get; set; }
    [field: SerializeField] public float TotalCoolTime { get; set; }
    public float Damage { get; set; }
    public bool IsArea { get; set; }
    public bool IsPenetration { get; set; }
    public float SkillRange { get; set; }
    public float Duration { get; set; }
    [field: SerializeField] public GameObject bulletPrefab { get; set; }
    private GameObject bulletObject;
    public float Scope { get; set; }
    public float MotionDelay { get; set; }
    public float Border = 3f;

    public float currentCoolTime = 0f;
    public SkillDataSO skilldata;
    public List<Bullet> bullets;
    public Bullet bullet;
    [field: SerializeField] public bool isAttackable { get; set; } = true; // 스킬 사용 가능 여부(쿨타임 포함)
    [field: SerializeField] public bool isAttacking { get; set; } = false; // 공격 중 여부

    public virtual void Start()
    {
        // Setting target is work on StartAttack();
        Character = gameObject.GetComponent<Character>();
        TotalCoolTime = skilldata.CoolTime;
        Damage = skilldata.Damage;
        SkillRange = skilldata.SkillRange;
        Duration = skilldata.Duration;
        bulletPrefab = skilldata.bulletPrefab;
        Scope = skilldata.Scope;
        MotionDelay = skilldata.MotionDelay;

        if (bulletPrefab != null)
        {
            // 폼의 인스턴트화
            /*
            Form = BattleManager.Instance.GetAttack(Form);
            */
            bullet = bulletPrefab.GetComponent<Bullet>();
            // bullet.InitData(skilldata, Character);
        }
    }

    private void Update()
    {
        if (currentCoolTime > 0f && isAttackable == false)
        {
            currentCoolTime -= Time.deltaTime;
        } else if (currentCoolTime <= 0f && isAttackable == false)
        {
            isAttackable = true;
        }
    }

    public virtual void ResetCoolTime()
    {
        currentCoolTime = TotalCoolTime;
        Debug.Log(this + "Used");
    }

    #region AnimationFunc

   public virtual void StartAttack()
    {
       Target = Character.Target.GetComponent<Character>();
        ResetCoolTime();
    }

    public virtual void EndAttack()
    {
        isAttacking = false;
    }

    public virtual void AttackTiming()
    {
        var direction = Target.transform.position - transform.position;

        if (bullet.gameObject == null)
            return;
        bulletObject = Instantiate(bullet.gameObject, position: transform.position + Vector3.up, Quaternion.identity);

        if (bulletObject != null)
        {
            bulletObject.transform.position = new Vector3(transform.position.x, transform.position.y , transform.position.z);
            bulletObject.GetComponent<Bullet>().SetData(Target.gameObject, Damage);
            bulletObject.GetComponent<Bullet>().Shoot();

            bulletObject.SetActive(true);
        }
        
        
        // bulletObject.GetComponent<Rigidbody>().AddForce(direction * 100);
        /*
        if (Character.Renderer.flipX)
        {
            Form.transform.position = new Vector3 (transform.position.x + 2f, transform.position.y+ Border, transform.position.z);    
        } else
        {
            Form.transform.position = new Vector3(transform.position.x - 2f, transform.position.y + Border, transform.position.z);
        }
        */
    }

    public virtual void StartRestriction()
    {
        // Debug.Log("Start Restriction");

        Character.IsRestriction = true;
    }

    public virtual void EndRestriction()
    {
        // Debug.Log("End Restriction");
        Character.IsRestriction = false;
    }

    #endregion
}
