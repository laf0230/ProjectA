using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour, SkillState
{
    [field: SerializeField] public Character SelfCharacter { get; set; }
    public Character Target { get; set; }
    [field: SerializeField] public float CoolTime { get; set; }
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
    [field: SerializeField] public bool isAttackable { get; set; } = true;
    [field: SerializeField] public bool isAttacking { get; set; } = false;
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
    04/20 ������ ����
    CharacterȤ�� �� ĳ���� Ŭ�������� ��ų���� �������� ���ϰ� ����.
    ����, ��ų, ����� ��ų�� SkillBaseŬ������ ��ӹް�
    �� ĳ���Ͱ� ����� �� �ֵ��� ĳ���� �������� ����ϰ� ����

    �׷��� Character���� �� ĳ������ ����, ��ų ����� ��ų Ŭ����
    ��) SwordMan: Slash, Bash, Throw,
        Achor: Shoot, Swing, ChargeShoot
    �� �����ϴ� ����� ã�ƾ���
     */

    private void Start()
    {
        // Setting target is work on StartAttack();
        SelfCharacter = gameObject.GetComponent<Character>();
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
        if(currentCoolTime > 0 && !isAttackable)
        {
            currentCoolTime -= Time.deltaTime;
        } else if (currentCoolTime  <= 0f && !isAttackable)
        {
            currentCoolTime = CoolTime;
            isAttackable = true; 
        }
    }

    public virtual void ResetCoolTime()
    {
        currentCoolTime = CoolTime;
    }

    #region AnimationFunc

   public virtual void StartAttack()
    {
        Debug.Log("StartAttack");
       Target = SelfCharacter.Target.GetComponent<Character>();
        isAttacking = true;
    }

    public virtual void EndAttack()
    {
        Debug.Log("EndAttack");
        isAttackable = false;
        isAttacking = false;
    }

    public virtual void AttackTiming()
    {
        Debug.Log("Attack Timing");

//        Target.Damage(damageAmount: Damage);
        // Instantiate(Form.GetComponent<DamageTransfer>().skilldata = this.skilldata);
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
}
