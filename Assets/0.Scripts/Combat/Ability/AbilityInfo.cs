using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

// ��ų�� �ش� �����Ƽ�� ���ȴٴ� ����.
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
    [Header("������ �ٲٴ� �����Ƽ�� �� ���")]
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

// ���������� ��ų�� ���Ǵ� �����Ƽ ���
/*
ability�� ��ӹ޴� �ɷµ鿡�� Info Ȥ�� Target �������� ����� �� �ִٴ� ���� �����ֱ� ���� �����

abstract�� ��ӹ޴� Ŭ������ abstractŰ���尡 ���� ��� ������ �����ؾ���
����
������ �����ϰ� ������ �� ����
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
            // 1�ʿ� Info.Value��ŭ ������ �ο�

            Character.Damage(Info.Value);
            yield return new WaitForSeconds(1f);
            elapsedTime += 1f;
        }
    }
}


// �������ͽ� ��ȭ
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

    // �⺻ ���ݸ� ����
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
