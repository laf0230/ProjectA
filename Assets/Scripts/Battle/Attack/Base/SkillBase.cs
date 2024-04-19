using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour, SkillState
{
    public Character SelfCharacter { get; set; }
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
    AttackState에서 attack, skill, SS을 enum을 통해서 사용할 걸 바꾸는 방식
    Basic Skill Running
    */

    /*
    오브젝트 사용 방법   
    - ScriptableObject를 사용 스킬의 데이터를 저장
    - 사용할 때마다 바뀌는 스테이터스를 적용
    - attackTiming이 호출되면 오브젝트 소환과 함께 공격 애니메이션 사용
     */

    private void Awake()
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

        Target.Damage(damageAmount: Damage);
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
