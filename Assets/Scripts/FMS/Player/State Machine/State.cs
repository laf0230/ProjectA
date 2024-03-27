using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
    protected Character character;
    protected StateMachine stateMachine;

    public State(Character character, StateMachine stateMachine)
    {
        this.character = character;
        this.stateMachine = stateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent(Character.TriggerType triggerType) { }
}
