using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterChaseState : State
{
    private Transform _targetTransform;
    private Character _targetCharacter;
    private float _moveSpeed = 1.75f;

    public CharacterChaseState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void AnimationTriggerEvent(Character.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        AnimationTriggerEvent(Character.AnimationTriggerType.Run);

        _targetTransform = character.Target.transform;

        _moveSpeed = character.ChaseSpeed;

        _targetCharacter = character.Target.GetComponent<Character>();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (character.ThreatLevel < _targetCharacter.ThreatLevel)
        {
            character.IsPassThrough = true;
        }
        else
        {
            character.IsPassThrough = false;
        }

        if (!_targetCharacter.gameObject.activeSelf || !character.IsAggroed)
        {
            // ÀûÀÌ »ç¸ÁÇßÀ» ¶§
            character.StateMachine.ChangeState(character.IdleState);
        }

        if (character.IsPassThrough)
        {
            // µµ¸Á Ä¥ ¶§
            // PassThrough
            character.StateMachine.ChangeState(character.EscapeState);
        }

        character.MoveTo(((_targetTransform.position - character.transform.position) * _moveSpeed).normalized, _moveSpeed);
        if (character.IsWithinstrikingDistance && (character.Attack.isAttackable || character.Skill.isAttackable || character.SpecialSkill.isAttackable))
        {
            Debug.Log($"Character:{character} -- Attack: {character.Attack.isAttackable}, Skill: {character.Skill.isAttackable}, SpecialSkill: {character.SpecialSkill.isAttackable}");
            character.StateMachine.ChangeState(character.AttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}