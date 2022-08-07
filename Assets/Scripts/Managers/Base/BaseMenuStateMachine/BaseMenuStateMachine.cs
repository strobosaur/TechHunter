using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMenuStateMachine : MonoBehaviour
{
    public BaseMenuState CurrentState { get; private set; }

    // INITIALIZE STATE MACHINE
    public void Initialize(BaseMenuState state)
    {
        CurrentState = state;
        CurrentState.Enter();
    }

    // CHANGE STATE
    public void ChangeState(BaseMenuState state)
    {
        CurrentState.Exit();
        CurrentState = state;
        CurrentState.Enter();
    }
}
