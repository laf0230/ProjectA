using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// 타격 범위의 형태
public enum Shape
{
    Circle,
    Rect
}

// 어빌리티가 적용되는 형태, 범위
public class RangeType
{
    Shape shape;
    float range;
}

// 
public interface MovableAbility
{
    public enum MoveType
    {
        Teleport,
        Dash
    }

    public MoveType moveType { get; set; }
    public float range { get; set; }

}

// 어빌리티의 타겟
public enum TargetType_
{
    Self,
    Enemy,
    Enemies
}

public enum MovementActionType
{
    Dash,
    Teleport
}

// 스킬에 해당 어빌리티가 사용된다는 정보.
[System.Serializable]
public class AbilityInfo
{
    public string Name;
    public int ID;
    public bool IsPercentage;
    public bool HasMovement;
    public float Value;
    public float Duration;
    public TargetType_ TargetType;
    public Shape shape;
    public AnimationType AnimationType;
    [Header("스텟을 바꾸는 어빌리티일 때 사용")]
    public StatusList EffectStatus = new StatusList();
    public MovementActionType MovementActionType;
}

public interface IAbility
{
    public AbilityInfo Info { get; set; }
    public MonoBehaviour MonoBehaviour { get; set; }
    public void Initialize(AbilityInfo info) { }
    public virtual void Use() { }
}

// 실질적으로 스킬에 사용되는 어빌리티 기능
/*
ability를 상속받는 능력들에서 Info 혹은 Target 변수들을 사용할 수 있다는 것을 보여주기 위해 사용함

abstract을 상속받는 클래스는 abstract키워드가 있을 경우 무조건 구현해야함
장점
구현이 가능하고 강제할 수 있음
 */

public abstract class Ability_
{
    public abstract AbilityInfo Info { get; set; }
    public MonoBehaviour MonoBehaviour { get; set; }

    public abstract Transform Target { get; set; }

    public virtual void Initialize(AbilityInfo Info, MonoBehaviour MonoBehaviour)
    {
        this.Info = Info;
        this.MonoBehaviour = MonoBehaviour;
    }

    // Virtual use method to be overridden by specific abilities
    public abstract void use(Transform Target);

    // 타겟이 다수일 경우
    public void use(List<Transform> Targets)
    {
        foreach (Transform Target in Targets)
        {
            use(Target);
        }
    }

}

// 어빌리티의 기능별 구현
/* 
모든 능력마다 요구하는 변수가 다름
능력이 많아질 수록 모든 예외처리를 하기 어려워짐
이를 해결하기 위해 Monobehavior만을 사용하는 방식 채용
*/

public class Invisible : Ability_
{
    public override AbilityInfo Info { get; set; }
    public override Transform Target { get; set; }

    private SpriteRenderer renderer;
    private Color unInvisibleColor = new Color(1, 1, 1, 1);
    private Color invisibleColor = new Color(1, 1, 1, 0.5f);


    public override void Initialize(AbilityInfo Info, MonoBehaviour MonoBehaviour)
    {
        base.Initialize(Info, MonoBehaviour);
    }

    // Override the use method for Invisible
    public override void use(Transform Target)
    {
        this.Target = Target;
        renderer = Target.GetComponentInChildren<SpriteRenderer>();

        Debug.Log($"Invisibility activated for {Info.Duration} seconds!");
        MonoBehaviour.StartCoroutine(InvisibleAbility());
    }

    private IEnumerator InvisibleAbility()
    {
        renderer.color = invisibleColor;
        yield return new WaitForSeconds(Info.Duration);
        renderer.color = unInvisibleColor;
    }
}

public class Poison : Ability_
{

    public override AbilityInfo Info { get; set; }
    public override Transform Target { get; set; }
    public Character Character { get; set; }

    public override void Initialize(AbilityInfo Info, MonoBehaviour MonoBehaviour)
    {
        base.Initialize(Info, MonoBehaviour);
    }

    public override void use(Transform Target)
    {
        this.Target = Target;
        Character = this.Target.GetComponent<Character>();
        MonoBehaviour.StartCoroutine(PoisionAbility());
    }

    IEnumerator PoisionAbility()
    {
        float elapsedTime = 0f;
        while (Info.Duration > elapsedTime)
        {
            // 1초에 Info.Value만큼 데미지 부여

            Character.Damage(Info.Value);
            yield return new WaitForSeconds(1f);
            elapsedTime += 1f;
        }
    }
}


// 스테이터스 변화
public class StatusTransition : Ability_
{
    private Character targetCharacter;
    public override AbilityInfo Info { get; set; }
    public override Transform Target { get; set; }

    public override void use(Transform Target)
    {
        targetCharacter = Target.GetComponent<Character>();

        switch (Info.EffectStatus)
        {
            case StatusList.Health:
                ChangeHealth();
                break;
            case StatusList.Speed:
                ChangeSpeed();
                break;
            case StatusList.AttackSpeed:
                ChangeAttackSpeed();
                break;
        }
    }

    private void ChangeHealth()
    {
        // Assuming Info.Value is the amount of health to change, 
        // and Info.IsPercentage determines if it's a percentage or a flat value
        if (Info.IsPercentage)
        {
            float percentageChange = Info.Value / 100f;
            targetCharacter.CurrentHealth += targetCharacter.Status.MaxHealth * percentageChange;
        }
        else
        {
            targetCharacter.CurrentHealth += Info.Value;
        }
    }

    private void ChangeSpeed()
    {
        if (Info.IsPercentage)
        {
            float percentageChange = Info.Value / 100f;
            targetCharacter.Status.ChaseSpeed = percentageChange;
        }
        else
        {
            targetCharacter.Status.ChaseSpeed += Info.Value;
        }
    }

    // 기본 공격만 적용
    private void ChangeAttackSpeed()
    {
        if (Info.IsPercentage)
        {
            float percentageChange = Info.Value / 100f;
            targetCharacter.Status.AttackSpeed += targetCharacter.Status.AttackSpeed * percentageChange;
        }
        else
        {
            targetCharacter.Status.AttackSpeed += Info.Value;
        }
    }
}
