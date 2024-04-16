using Assets.Scripts.FMS.Attack.SkillType;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackState : State
{
    private Transform _transform;
    private Character _target;

    public SkillState SkillState;
    public Attack Attack;
    public Skill Skill;
    public SpcialSkill SpcialSkill;
   
    public CharacterAttackState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
        _transform = GameObject.FindGameObjectWithTag("Character").transform;
    }

    public override void AnimationTriggerEvent(Character.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        // for Skill State

        _target = character.Target.GetComponent<Character>();

        AnimationTriggerEvent(Character.AnimationTriggerType.Attack);

        character.Animator.SetTrigger("Attack");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (_target.gameObject.activeSelf && character.IsPassThrough)
        {
            // 적이 살아있으나 위협도가 높은 경우 도망
            character.StateMachine.ChangeState(character.EscapeState);
        }
        else if (!_target.gameObject.activeSelf)
        {
            // 적이 사망했을 경우, IDLE 상태로 변경
            character.StateMachine.ChangeState(character.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    #region Animation

    public override void EnterAnim()
    {
        base.EnterAnim();
    }

    public void AttackTiming()
    {
        // _target.Damage();
    }

    public override void ExitAnim()
    {
        base.ExitAnim();
    }

    #endregion
}
