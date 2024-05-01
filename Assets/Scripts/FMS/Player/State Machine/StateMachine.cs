using System.Collections;
using UnityEngine;

public class StateMachine
{
    public State CurrentPlayerState { get; set; }
    
    public void Initialize(State startingState)
    {
        CurrentPlayerState = startingState;
        CurrentPlayerState.EnterState();
    }

    public void ChangeState(State newState)
    {
        CurrentPlayerState.ExitState();
        CurrentPlayerState = newState;
        CurrentPlayerState.EnterState();
    }
}
