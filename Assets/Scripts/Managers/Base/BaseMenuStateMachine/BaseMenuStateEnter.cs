using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMenuStateEnter : BaseMenuState
{
    public BaseMenuStateEnter(BaseManager manager, BaseMenuStateMachine stateMachine) : base(manager, stateMachine){}

    public override void Enter()
    {
        base.Enter();

        manager.TogglePlayerMenu(false);
        manager.ToggleUpgradeMenu(false);
        manager.missionManager.ToggleMissionMenu(false);
        manager.missionManager.GenerateMissions();
        stateMachine.ChangeState(manager.stateIdle);
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
