using System.Collections;
using UnityEngine;

public class StateMachine
{
    public State CurrentPlayerState { get; set; }
    
    public void Initialize(State startingState)
    {
        Debug.Log("Current State: " + startingState.ToString());
        CurrentPlayerState = startingState;
        CurrentPlayerState.EnterState();
    }

    public void ChangeState(State newState)
    {
        Debug.Log("Current State: " + newState.ToString());
        CurrentPlayerState.ExitState();
        CurrentPlayerState = newState;
        CurrentPlayerState.EnterState();
    }
}
