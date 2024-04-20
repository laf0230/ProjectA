using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour, SkillState
{
    [field: SerializeField] public Character SelfCharacter { get; set; }
    public Character Target { get; set; }
    public float CoolTime { get; set; }
    public float Damage { get; set; }
    public bool IsArea { get; set; }
    public bool IsPenetration { get; set; }
    public float SkillRange { get; set; }
    public float Duration { get; set; }
    public GameObject Form { get; set; }
    public float Scope { get; set; }
    public float MotionDelay { get; set; }

    public float currentCoolTime;
    public SkillDataSO skilldata;
    public bool isAttackable { get; set; }
    /*
    AttackState���� attack, skill, SS�� enum�� ���ؼ� ����� �� �ٲٴ� ���
    Basic Skill Running
    */

    /*
    ������Ʈ ��� ���   
    - ScriptableObject�� ��� ��ų�� �����͸� ����
    - ����� ������ �ٲ�� �������ͽ��� ����
    - attackTiming�� ȣ��Ǹ� ������Ʈ ��ȯ�� �Բ� ���� �ִϸ��̼� ���
     */

    /*
    private void Start()
    {
        SelfCharacter = gameObject.GetComponent<Character>();
        Target = SelfCharacter.Target.GetComponent<Character>();
        CoolTime = skilldata.CoolTime;
        Damage = skilldata.Damage;
        SkillRange = skilldata.SkillRange;
        Duration = skilldata.Duration;
        Form = skilldata.Form;
        Scope = skilldata.Scope;
        MotionDelay = skilldata.MotionDelay;

        if (Target == null)
        {
            SelfCharacter.StateMachine.ChangeState(SelfCharacter.IdleState);
        }
    }

    private void Update()
    {
        if(CoolTime > 0 && !isAttackable)
        {
            CoolTime -= Time.deltaTime;
        } else if (CoolTime  < 0 && !isAttackable)
        {
            isAttackable = true;        
        }
    }

    public virtual void ResetCoolTime(float _coolTime)
    {
        CoolTime = _coolTime;
    }

    #region AnimationFunc

    public virtual void StartAttack()
    {
        Debug.Log("StartAttack");
    }

    public virtual void EndAttack()
    {
        Debug.Log("EndAttack");
        this.enabled = false;
    }

    public virtual void AttackTiming()
    {
        Debug.Log("Attack Timing");

//        Target.Damage(damageAmount: Damage);
        Instantiate(Form.GetComponent<DamageTransfer>().skilldata = this.skilldata);
    }

    public virtual void StartRestriction()
    {
        Debug.Log("Start Restriction");

        SelfCharacter.IsRestriction = true;
    }

    public virtual void EndRestriction()
    {
        Debug.Log("End Restriction");
        SelfCharacter.IsRestriction = false;
    }

    #endregion
    */
}
