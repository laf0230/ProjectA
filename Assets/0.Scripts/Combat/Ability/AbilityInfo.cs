using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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

public interface IAbility
{
     string Name { get; set; }
     int ID { get; set; }
     bool IsPercentage { get; set; }
     float Value { get; set; }
     float Duration { get; set; }
     TargetType_ TargetType { get; set; }
     Shape Shape { get; set; }
     AnimationType AnimationType { get; set; }
     StatusList EffectStatus { get; set; }
}

// 스킬에 해당 어빌리티가 사용된다는 정보.
[System.Serializable]
public class AbilityInfo : IAbility
{
    [SerializeField] private string name;
    [SerializeField] private int id;
    [SerializeField] private bool isPercentage;
    [SerializeField] private float value;
    [SerializeField] private float duration;
    [SerializeField] private TargetType_ targetType;
    [SerializeField] private Shape shape;
    [SerializeField] private AnimationType animationType;
    [Header("Id가 0일 때만 사용")]
    [SerializeField] private StatusList effectStatus; // id가 0일 때만 보여줌

    #region Getter / Setter
   
    public string Name { get => name; set => name = value; }
    public int ID { get => id; set => id = value; }
    public bool IsPercentage { get => isPercentage; set => isPercentage = value; }
    public float Value { get => value; set => this.value = value; }
    public float Duration { get => duration; set => duration = value; }
    public TargetType_ TargetType { get => targetType; set => targetType = value; }
    public Shape Shape { get => shape; set => shape = value; }
    public AnimationType AnimationType { get => animationType; set => animationType = value; }
    public StatusList EffectStatus { get => effectStatus; set => effectStatus = value; }

    #endregion
}

// 실질적으로 스킬에 사용되는 어빌리티 기능
/*
ability를 상속받는 능력들에서 Info 혹은 Target 변수들을 사용할 수 있다는 것을 보여주기 위해 사용함

abstract을 상속받는 클래스는 abstract키워드가 있을 경우 무조건 구현해야함
장점
구현이 가능하고 강제할 수 있음
 */

public abstract class Ability
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
// 스테이터스 변화
public class StatTransition : Ability
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

        // Debug.Log($"{Info.EffectStatus}스테이터스가 변화되었습니다.!");
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
            
            // targetCharacter.Status.AttackSpeed += targetCharacter.Status.AttackSpeed * percentageChange;
        }
        else
        {
            // targetCharacter.Status.AttackSpeed += Info.Value;
        }
    }
}

public class Invisible : Ability
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

public class Poison : Ability
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

// 침묵
public class Silence : Ability
{
    public override AbilityInfo Info { get; set; }
    public override Transform Target { get; set; }

    public override void Initialize(AbilityInfo Info, MonoBehaviour MonoBehaviour)
    {
        base.Initialize(Info, MonoBehaviour);
    }

    public override void use(Transform Target)
    {
        var Character = Target.GetComponent<Character>();

        MonoBehaviour.StartCoroutine(IESilence(Character));
    }

    IEnumerator IESilence(Character character)
    {
        character.IsAttackable = false;
        yield return null;
    }
}

public class Stun : Ability
{
    public override AbilityInfo Info { get; set; }
    public override Transform Target { get; set;}

    public override void use(Transform Target)
    {
        var Character = Target.GetComponent<Character>();
        MonoBehaviour.StartCoroutine(IEStun(Character, Info.Duration));
    }
    
    // Skill Stun
    public IEnumerator IEStun(Character target, float stunTime)
    {
        target.SetMoveAble(true);
        yield return new WaitForSeconds(stunTime);
        target.SetMoveAble(false);
    }
}
