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

        // SET CAMERA STATE
        manager.cam.stateMachine.ChangeState(manager.cam.stateBase);

        PlayerManager.instance.playerStats.HPcur = PlayerManager.instance.playerStats.HPmax.GetValue();

        manager.cam.transform.position = new Vector3(manager.player.transform.position.x,manager.player.transform.position.y,manager.cam.transform.position.z);

        manager.cam.dustPS.SetActive(false);
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
