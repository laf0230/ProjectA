using System.Collections;
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

    public MoveType moveType {  get; set; }
    public float range {  get; set; }

}

// �����Ƽ�� Ÿ��
public enum TargetType_
{
    Self,
    Enemy,
    Enemies
}

// ��ų�� �ش� �����Ƽ�� ���ȴٴ� ����.
[System.Serializable]
public class AbilityInfo
{
    public int ID;
    public string Name;
    public float Value;
    public float Duration;
    public Shape shape;
    public AnimationType AnimationType;
}

public interface IAbility
{
    public AbilityInfo Info { get; set; }
    public MonoBehaviour MonoBehaviour {  get; set; }
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
    public abstract AbilityInfo Info {  get; set; }
    public MonoBehaviour MonoBehaviour { get; set; }

    public abstract Transform Target { get; set; }

    public Ability_(MonoBehaviour MonoBehaviour)
    {
        this.MonoBehaviour = MonoBehaviour;
    }

    public virtual void SetTarget(Transform target) {this.Target = target;}

    public virtual void Initialize(AbilityInfo Info)
    {
        this.Info = Info;
    }

    // Virtual use method to be overridden by specific abilities
    public abstract void use();
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

    public Invisible(MonoBehaviour MonoBehaviour) : base(MonoBehaviour)
    {
        renderer = Target.GetComponentInChildren<SpriteRenderer>();
    }

    // Override the use method for Invisible
    public override void use()
    {
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
    public Poison(MonoBehaviour MonoBehaviour) : base(MonoBehaviour)
    {
    }

    public override void SetTarget(Transform target)
    {
        base.SetTarget(target);

        Character = target.GetComponent<Character>();
    }

    public override void use()
    {
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
