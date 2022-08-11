using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManagerLevelWon : ManagerState
{
    public StateManagerLevelWon(GameManager manager, StateMachine stateMachine) : base(manager, stateMachine){}

    // ON STATE ENTER
    public override void Enter()
    {
        base.Enter();

        manager.blackscreen.OnBlackScreenBlack += ChangeSceneBase;

        // CLEAR LISTS
        PlayerManager.instance.playerList.Clear();
        PropGenerator.instance.ClearAllProps();
        EnemyManager.instance.spawnPointGenerator.DeleteAllSpawnPoints();
        EnemyManager.instance.ClearAllEnemies();
    }

    // ON STATE EXIT
    public override void Exit()
    {
        base.Exit();
    }

    // STATE LOGIC UPDATE
    public override void LogicUpdate()
    {
        base.LogicUpdate();        
    }

    // STATE PHYSICS UPDATE
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // CHANGE STATE BACK TO BASE
    private void ChangeSceneBase()
    {
        manager.blackscreen.OnBlackScreenBlack -= ChangeSceneBase;

        // LOAD BASE SCENE
        manager.levelManager.LoadScene(manager.levelManager.sceneNames[(int)SceneName.InBase]);
    }
}
