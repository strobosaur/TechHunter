using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
   public ManagerState CurrentState { get; private set; }

   public void Iinitialize(ManagerState startingState)
   {
       CurrentState = startingState;
       CurrentState.Enter();
   }

   public void ChangeState(ManagerState newState)
   {
       CurrentState.Exit();
       CurrentState = newState;
       CurrentState.Enter();
   }
}