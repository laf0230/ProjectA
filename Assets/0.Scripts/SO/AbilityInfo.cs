using System.Collections;
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

    public MoveType moveType {  get; set; }
    public float range {  get; set; }

}

// 어빌리티의 타겟
public enum TargetType_
{
    Self,
    Enemy,
    Enemies
}

// 스킬에 해당 어빌리티가 사용된다는 정보.
[System.Serializable]
public class AbilityInfo
{
    public int ID;
    public float Value;
    public TargetType_ TargetType;
    public AnimationType AnimationType;
}

// 실질적으로 스킬에 사용되는 어빌리티 기능
public class Ability_
{
    public AbilityInfo info = new AbilityInfo();

    // Virtual use method to be overridden by specific abilities
    public virtual void use()
    {
        Debug.Log("Ability used!");
    }
}

// 어빌리티의 기능별 구현
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
