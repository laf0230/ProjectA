using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Ÿ�� ������ ����
public enum Shape
{
    Circle,
    Rect
}

// �����Ƽ�� ����Ǵ� ����, ����
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

// �����Ƽ�� Ÿ��
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

// ��ų�� �ش� �����Ƽ�� ���ȴٴ� ����.
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
    [Header("Id�� 0�� ���� ���")]
    [SerializeField] private StatusList effectStatus; // id�� 0�� ���� ������

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

// ���������� ��ų�� ���Ǵ� �����Ƽ ���
/*
ability�� ��ӹ޴� �ɷµ鿡�� Info Ȥ�� Target �������� ����� �� �ִٴ� ���� �����ֱ� ���� �����

abstract�� ��ӹ޴� Ŭ������ abstractŰ���尡 ���� ��� ������ �����ؾ���
����
������ �����ϰ� ������ �� ����
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

    // Ÿ���� �ټ��� ���
    public void use(List<Transform> Targets)
    {
        foreach (Transform Target in Targets)
        {
            use(Target);
        }
    }

}

// �����Ƽ�� ��ɺ� ����
/* 
��� �ɷ¸��� �䱸�ϴ� ������ �ٸ�
�ɷ��� ������ ���� ��� ����ó���� �ϱ� �������
�̸� �ذ��ϱ� ���� Monobehavior���� ����ϴ� ��� ä��
*/
// �������ͽ� ��ȭ
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

        // Debug.Log($"{Info.EffectStatus}�������ͽ��� ��ȭ�Ǿ����ϴ�.!");
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

    // �⺻ ���ݸ� ����
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
            // 1�ʿ� Info.Value��ŭ ������ �ο�

            Character.Damage(Info.Value);
            yield return new WaitForSeconds(1f);
            elapsedTime += 1f;
        }
    }
}

// ħ��
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
