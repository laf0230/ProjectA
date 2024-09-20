using System.Collections;
using UnityEngine;

public enum MovementType
{
    Teleport,
    Dash
}

public enum TargetType
{
    self,
    enemy
}

public enum shapeType
{
    Circle
}

public class EffectArea
{
    public shapeType shapeType;
    public float Range;
}

public interface IMoveableAbility
{
    public MovementType Type {  get; set; }
    public float Distance { get; set; }
}

public class Ability
{
    public string Name {  get; set; }

    public Ability(string name, Transform target)
    {
        Name = name;
        this.target = target;
    }

    public Transform target;

    public virtual void Use() { }
}

public class Hide : Ability
{
    SpriteRenderer spriteRenderer;
    bool isHide;
    float duration;

    public Hide(string name, Transform target, SpriteRenderer spriteRenderer) : base(name, target)
    {
        this.spriteRenderer = spriteRenderer;
    }

    public override void Use()
    {
        Clocking();
    }

    public IEnumerator Clocking()
    {
        yield return new WaitForSeconds(duration);
    }
}