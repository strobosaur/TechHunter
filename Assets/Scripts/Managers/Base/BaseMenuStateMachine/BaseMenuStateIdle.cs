using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMenuStateIdle : BaseMenuState
{
    public BaseMenuStateIdle(BaseManager manager, BaseMenuStateMachine stateMachine) : base(manager, stateMachine){}

    public override void Enter()
    {
        base.Enter();

        manager.TogglePlayerMenu(false);
        manager.ToggleUpgradeMenu(false);
        manager.missionManager.ToggleMissionMenu(false);
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
