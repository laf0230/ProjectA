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
    public float Value;
    public TargetType_ TargetType;
    public AnimationType AnimationType;
}

// ���������� ��ų�� ���Ǵ� �����Ƽ ���
public class Ability_
{
    public AbilityInfo info = new AbilityInfo();

    // Virtual use method to be overridden by specific abilities
    public virtual void use()
    {
        Debug.Log("Ability used!");
    }
}

// �����Ƽ�� ��ɺ� ����
public class Invisible : Ability_
{
    private SpriteRenderer renderer;
    private Transform target;
    public float invisibilityDuration;
    private MonoBehaviour mono;
    private Color unInvisibleColor = new Color();
    private Color invisibleColor = new Color();
    
    public Invisible (Transform target, float InvisibilityDuration, MonoBehaviour mono)
    {
        renderer = target.GetComponent<SpriteRenderer>();
        this.invisibilityDuration = InvisibilityDuration;
        this.mono = mono;
    }

    // Override the use method for Invisible
    public override void use()
    {
        Debug.Log($"Invisibility activated for {invisibilityDuration} seconds!");
        mono.StartCoroutine(InvisibleAbility());
    }

    private IEnumerator InvisibleAbility()
    {
        renderer.color = invisibleColor;
        yield return new WaitForSeconds( invisibilityDuration );
        renderer.color = unInvisibleColor;
    }
}
