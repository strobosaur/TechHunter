using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManagerLevel : ManagerState
{
    public StateManagerLevel(GameManager manager, StateMachine stateMachine) : base(manager, stateMachine){}

    public override void Enter()
    {
        base.Enter();

        // SUBSCRIBE TO SCENE CHANGED
        SceneManager.sceneLoaded += InitLevelScene;
    }

    public override void Exit()
    {
        base.Exit();
        PlayerManager.instance.playerList.Clear();

        // UNSUBSCRIBE TO SCENE CHANGED
        SceneManager.sceneLoaded -= InitLevelScene;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // FIND PLAYER
        if (manager.player == null) manager.player = GameObject.Find(Globals.G_PLAYERNAME).GetComponent<Player>();
        if (PlayerManager.instance.playerList.Count < 1) {
            PlayerManager.instance.FindPlayers();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void InitLevelScene(Scene s, LoadSceneMode mode)
    {
        Debug.Log("Level Scene Loaded: " + s.name);
        InitializeLevel();
    }

    public void InitializeLevel()
    {
        // BLACKSCREEN FADE IN
        manager.blackscreen.StartBlackScreenFade(false);

        // FIND PLAYERS
        manager.player = GameObject.Find(Globals.G_PLAYERNAME).GetComponent<Player>();
        PlayerManager.instance.FindPlayers();

        // SET CAMERA STATE
        manager.cam.stateMachine.ChangeState(manager.cam.stateLevel);
        
        // SETUP ENEMIES & GENERATE MAP
        EnemyManager.instance.InitEnemyManager();
        GameObject.Find("MapManager").GetComponent<MapManager>().GenerateMapRNG();
    }
}
