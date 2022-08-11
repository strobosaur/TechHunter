using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManagerBase : ManagerState
{
    public StateManagerBase(GameManager manager, StateMachine stateMachine) : base(manager, stateMachine){}

    public override void Enter()
    {
        base.Enter();

        // BLACKSCREEN FADE IN
        manager.blackscreen.StartBlackScreenFade(false);

        // FIND PLAYER
        PlayerManager.instance.FindPlayers();
        manager.player = GameObject.Find(Globals.G_PLAYERNAME).GetComponent<Player>();
        manager.player.crosshair.ToggleVisibility(false);
        //PlayerManager.instance.playerStats.HPcur = manager.player.stats.HPmax.GetValue();

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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
