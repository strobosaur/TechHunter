using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManagerBase : ManagerState
{
    public StateManagerBase(GameManager manager, StateMachine stateMachine) : base(manager, stateMachine){}

    public override void Enter()
    {
        base.Enter();

        // BLACKSCREEN FADE IN
        manager.blackscreen.StartBlackScreenFade(false);

        // FIND PLAYER
        manager.player = GameObject.Find(Globals.G_PLAYERNAME).GetComponent<Player>();

        // SET CAMERA STATE
        manager.cam.stateMachine.ChangeState(manager.cam.stateBase);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // FIND PLAYER
        if (manager.player == null) manager.player = GameObject.Find(Globals.G_PLAYERNAME).GetComponent<Player>();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
