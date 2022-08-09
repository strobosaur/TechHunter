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

        // SUBSCRIBE TO SCENE CHANGED EVENT
        SceneManager.sceneLoaded += InitBaseScene;
    }

    public override void Exit()
    {
        base.Exit();

        // SUBSCRIBE TO SCENE CHANGED EVENT
        SceneManager.sceneLoaded -= InitBaseScene;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // INIT LEVEL SCENE
    public void InitBaseScene(Scene s, LoadSceneMode mode)
    {
        Debug.Log(" INIT BASE SCENE ");

        // BLACKSCREEN FADE IN
        manager.blackscreen.StartBlackScreenFade(false);
        
        // FIND PLAYER
        manager.player = GameObject.Find(Globals.G_PLAYERNAME).GetComponent<Player>();

        // SET CAMERA STATE
        manager.cam.stateMachine.ChangeState(manager.cam.stateBase);
    }
}
