using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateManagerStartGame : ManagerState
{
    // CONSTRUCTOR
    public StateManagerStartGame(GameManager manager, StateMachine stateMachine) : base(manager, stateMachine){}

    // ENTER
    public override void Enter()
    {
        base.Enter();
    }

    // EXIT
    public override void Exit()
    {
        base.Exit();
    }

    // LOGIC UPDATE
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // LOAD MENU SCENE
        manager.levelManager.LoadScene(manager.levelManager.sceneNames[(int)SceneName.MainMenu]);

        // TEMP
        // PlayerManager.instance.ResetGameSession();
        // manager.levelManager.LoadScene(manager.levelManager.sceneNames[(int)SceneName.InLevel]);
    }

    // PHYSICS UPDATE
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
