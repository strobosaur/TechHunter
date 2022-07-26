using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    // CURRENT STATE HOLDER
    public ManagerState CurrentState { get; private set; }

    // INITIALIZE STATE MACHINE
    public void Initialize(ManagerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    // CHANGE STATE
    public void ChangeState(ManagerState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}