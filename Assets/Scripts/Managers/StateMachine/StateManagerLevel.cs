using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManagerLevel : ManagerState
{
    public StateManagerLevel(GameManager manager, StateMachine stateMachine) : base(manager, stateMachine){}

    public override void Enter()
    {
        base.Enter();

        // SET CAMERA STATE
        manager.cam.stateMachine.ChangeState(manager.cam.stateLevel);
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
