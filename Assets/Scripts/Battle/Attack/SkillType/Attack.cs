using UnityEngine;

public class Attack : SkillBase
{
    private void OnEnable()
    {
        base.SelfCharacter.AnimationTriggerEvent(Character.AnimationTriggerType.Attack);
    }
/*
    public override void StartAttack()
    {
        base.StartAttack();

        Debug.Log("Attack");
    }
*/
}
