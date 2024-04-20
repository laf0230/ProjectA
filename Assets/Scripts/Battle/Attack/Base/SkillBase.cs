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
    AttackState에서 attack, skill, SS을 enum을 통해서 사용할 걸 바꾸는 방식
    Basic Skill Running
    */

    /*
    오브젝트 사용 방법   
    - ScriptableObject를 사용 스킬의 데이터를 저장
    - 사용할 때마다 바뀌는 스테이터스를 적용
    - attackTiming이 호출되면 오브젝트 소환과 함께 공격 애니메이션 사용
     */
    
    /*
    04/20 문제점 정리
    Character혹은 각 캐릭터 클래스에서 스킬들을 참조하지 못하고 있음.
    공격, 스킬, 스페셜 스킬은 SkillBase클래스를 상속받고
    각 캐릭터가 사용할 수 있도록 캐릭터 맞춤으로 상속하고 있음

    그런데 Character에서 각 캐릭터의 공격, 스킬 스페셜 스킬 클래스
    예) SwordMan: Slash, Bash, Throw,
        Achor: Shoot, Swing, ChargeShoot
    를 참조하는 방법을 찾아야함
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
