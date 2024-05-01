using UnityEngine;

public class Attack : SkillBase
{
    public override void StartAttack()
    {
        base.StartAttack();

        Character.AnimationTriggerEvent(Character.AnimationTriggerType.Attack);
        Debug.Log("Attack");
    }
}
