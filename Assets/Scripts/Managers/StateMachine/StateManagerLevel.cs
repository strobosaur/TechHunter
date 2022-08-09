using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManagerLevel : ManagerState
{
    public StateManagerLevel(GameManager manager, StateMachine stateMachine) : base(manager, stateMachine){}

    // ON STATE ENTER
    public override void Enter()
    {
        base.Enter();

        // SUBSCRIBE TO SCENE CHANGED
        SceneManager.sceneLoaded += InitLevelScene;
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

        // UNSUBSCRIBE TO SCENE CHANGED
        SceneManager.sceneLoaded -= InitLevelScene;
    }

    // STATE LOGIC UPDATE
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // FIND PLAYER
        if (manager.player == null) manager.player = GameObject.Find(Globals.G_PLAYERNAME).GetComponent<Player>();
        if (PlayerManager.instance.playerList.Count < 1) {
            PlayerManager.instance.FindPlayers();
        }
    }

    // STATE PHYSICS UPDATE
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // INITIALIZE LEVEL SCENE
    public void InitLevelScene(Scene s, LoadSceneMode mode)
    {
        InitializeLevel();
    }

    // INITIALIZE LEVEL
    public void InitializeLevel()
    {
        // BLACKSCREEN FADE IN
        manager.blackscreen.StartBlackScreenFade(false);

        // FIND PLAYERS
        manager.player = GameObject.Find(Globals.G_PLAYERNAME).GetComponent<Player>();
        PlayerManager.instance.FindPlayers();
        manager.player.crosshair.ToggleVisibility(true);

        // SET CAMERA STATE
        manager.cam.stateMachine.ChangeState(manager.cam.stateLevel);
        
        // SETUP ENEMIES & GENERATE MAP
        EnemyManager.instance.InitEnemyManager();
        GameObject.Find("MapManager").GetComponent<MapManager>().GenerateMapRNG();

        // START LEVEL
        CurrentLevelManager.instance.StartLevel(120 * (1f + (LevelManager.instance.difficulty * 0.1f)));
        CurrentLevelManager.instance.onLevelWon += LevelWon;

        SpawnPointManager.instance.StartSpawning();

        // INIT HUD
        HUDlevel.instance.UpdateHUD();
    }

    // HANDLE LEVEL WON
    private void LevelWon()
    {
        CurrentLevelManager.instance.onLevelWon -= LevelWon;

        manager.blackscreen.StartBlackScreenFade();
        manager.blackscreen.OnBlackScreenBlack += ChangeStateBase;
    }

    // CHANGE STATE BACK TO BASE
    private void ChangeStateBase()
    {
        // UNSUBSCRIBE TO EVENT
        manager.blackscreen.OnBlackScreenBlack -= ChangeStateBase;

        // CHANGE STATE TO BASE
        manager.stateMachine.ChangeState(manager.stateBase);

        // LOAD BASE SCENE
        manager.levelManager.LoadScene(manager.levelManager.sceneNames[(int)SceneName.InBase]);
    }
}
