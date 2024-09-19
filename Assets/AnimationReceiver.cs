using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationType
{
    Impact,
    End,
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
        Debug.Log(type);

        switch(type)
        {
            case AnimationType.Impact:
                Debug.Log("Attack--");
                character.AttackState.DoAttack();
                break;
            case AnimationType.End:
                Debug.Log("End Attack--");
                character.AttackState.EndAttack();
                break;
            case AnimationType.Ability:
                character.AttackState.EffectAbility();
                break;
        }
    }
}
