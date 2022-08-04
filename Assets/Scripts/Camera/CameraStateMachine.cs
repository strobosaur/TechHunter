using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateMachine
{
    // CURRENT STATE HOLDER
    public CameraState CurrentState { get; private set; }

    // INITIALIZE STATE MACHINE
    public void Initialize(CameraState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    // CHANGE STATE MACHINE STATE
    public void ChangeState(CameraState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
