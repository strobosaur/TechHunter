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

        SceneManager.sceneLoaded += InitBaseState;

        manager.blackscreen.OnBlackScreenBlack += ChangeStateBase;
    }

    // ON STATE EXIT
    public override void Exit()
    {
        base.Exit();

        // CLEAR LISTS
        PlayerManager.instance.playerList.Clear();
        PropGenerator.instance.ClearAllProps();
        EnemyManager.instance.spawnPointGenerator.DeleteAllSpawnPoints();
        EnemyManager.instance.ClearAllEnemies();
        manager.blackscreen.OnBlackScreenBlack -= ChangeStateBase;
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

    // INIT STATE BASE
    public void InitBaseState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= InitBaseState;

        // CHANGE STATE TO BASE STATE
        stateMachine.ChangeState(manager.stateBase);
    }

    // CHANGE STATE BACK TO BASE
    private void ChangeStateBase()
    {
        // CHANGE STATE TO BASE
        //manager.stateMachine.ChangeState(manager.stateBase);

        // LOAD BASE SCENE
        manager.levelManager.LoadScene(manager.levelManager.sceneNames[(int)SceneName.InBase]);
    }
}
