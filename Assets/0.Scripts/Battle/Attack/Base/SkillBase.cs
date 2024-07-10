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
    [field: SerializeField] public GameObject Form { get; set; }
    public float Scope { get; set; }
    public float MotionDelay { get; set; }
    public float Border = 3f;

    public float currentCoolTime = 0f;
    public SkillDataSO skilldata;
    public List<Bullet> bullets;
    public Bullet bullet;
    [field: SerializeField] public bool isAttackable { get; set; } = true; // ��ų ��� ���� ����(��Ÿ�� ����)
    [field: SerializeField] public bool isAttacking { get; set; } = false; // ���� �� ����

    public virtual void Start()
    {
        // Setting target is work on StartAttack();
        Character = gameObject.GetComponent<Character>();
        TotalCoolTime = skilldata.CoolTime;
        Damage = skilldata.Damage;
        SkillRange = skilldata.SkillRange;
        Duration = skilldata.Duration;
        Form = skilldata.Form;
        Scope = skilldata.Scope;
        MotionDelay = skilldata.MotionDelay;

        if (Form != null)
        {
            // ���� �ν���Ʈȭ
            /*
            Form = BattleManager.Instance.GetAttack(Form);
            */
            bullet = Form.GetComponent<Bullet>();
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
        Form = Instantiate(bullet.gameObject, position: transform.position + Vector3.up, Quaternion.identity);
        if (Form != null)
        {
            Form.SetActive(true);
        }

        Form.GetComponent<Rigidbody>().AddForce(direction * 100);
        /*
        if (Character.Renderer.flipX)
        {
            Form.transform.position = new Vector3 (transform.position.x + 2f, transform.position.y+ Border, transform.position.z);    
        } else
        {
            Form.transform.position = new Vector3(transform.position.x - 2f, transform.position.y + Border, transform.position.z);
        }
        */
        Form.transform.position = new Vector3(transform.position.x, transform.position.y , transform.position.z);
        Form.GetComponent<Bullet>().SetTarget(Target.gameObject);
        Form.GetComponent<Bullet>().damage = Damage;
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
