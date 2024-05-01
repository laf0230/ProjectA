using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour, SkillState
{
    [field: SerializeField] public Character Character { get; set; }
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

    public float currentCoolTime = 0f;
    public SkillDataSO skilldata;
    [field: SerializeField] public bool isAttackable { get; set; } = true; // ��ų ��� ���� ����(��Ÿ�� ����)
    [field: SerializeField] public bool isAttacking { get; set; } = false; // ���� �� ����
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

    public virtual void Start()
    {
        // Setting target is work on StartAttack();
        Character = gameObject.GetComponent<Character>();
        CoolTime = skilldata.CoolTime;
        Damage = skilldata.Damage;
        SkillRange = skilldata.SkillRange;
        Duration = skilldata.Duration;
        Form = skilldata.Form;
        Scope = skilldata.Scope;
        MotionDelay = skilldata.MotionDelay;

        if (Form != null)
        {
            // ���� �ν���Ʈȭ
            // Form = BattleManager.instance.GetAttack(Form);
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
       Target = Character.Target.GetComponent<Character>();
        isAttacking = true;
        isAttackable = false;
        ResetCoolTime();
    }

    public virtual void EndAttack()
    {
        isAttacking = false;
    }

    public virtual void AttackTiming()
    {

        // Target.Damage(damageAmount: Damage);
        // Instantiate(Form.GetComponent<DamageTransfer>().skilldata = this.skilldata);
        /*
        Form = BattleManager.instance.GetAttack(Form);
        Bullet bullet = Form.GetComponent<Bullet>(); 
        bullet.character = Character;
        bullet.InitData(skilldata);
        bullet.Shoot();
         */
    }

    public virtual void StartRestriction()
    {
        Debug.Log("Start Restriction");

        Character.IsRestriction = true;
    }

    public virtual void EndRestriction()
    {
        Debug.Log("End Restriction");
        Character.IsRestriction = false;
    }

    #endregion
}
