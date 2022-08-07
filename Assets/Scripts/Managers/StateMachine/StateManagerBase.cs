using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManagerBase : ManagerState
{
    public StateManagerBase(GameManager manager, StateMachine stateMachine) : base(manager, stateMachine){}

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
