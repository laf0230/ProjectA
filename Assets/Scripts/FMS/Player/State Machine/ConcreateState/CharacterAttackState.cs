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
            // ���� ��������� �������� ���� ��� ����
            character.StateMachine.ChangeState(character.EscapeState);
        }
        else if (!_target.gameObject.activeSelf)
        {
            // ���� ������� ���, IDLE ���·� ����
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
