using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationType
{
    Start,
    End,
    Impact,
    Ability
}

public class AnimationReceiver: MonoBehaviour
{
    Character character;

    private void Start()
    {
        character = GetComponentInParent<Character>();
    }

    public void AnimationTrigger(AnimationType type)
    {
        switch(type)
        {
            case AnimationType.Start:
                break;
            case AnimationType.Impact:
                character.AttackState.DoAttack();
                break;
            case AnimationType.End:
                character.AttackState.EndAttack();
                break;
            case AnimationType.Ability:
                character.AttackState.DoAbility();
                break;
        }
    }
}
