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
        InitializeLevel();
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
        CurrentLevelManager.instance.StartLevel(60 * (1f + (-Mathf.Pow(LevelManager.instance.difficulty * 0.005f, 2f)) + (LevelManager.instance.difficulty * 0.05f)));
        CurrentLevelManager.instance.onLevelWon += LevelWon;
        PlayerManager.instance.onGameOver += ScoreManager.instance.CreateHighscoreEntry;
        PlayerManager.instance.onGameOver += HandleGameOver;

        SpawnPointManager.instance.StartSpawning();

        // INIT HUD
        HUDlevel.instance.UpdateHUD();
    }

    // HANDLE LEVEL WON
    private void LevelWon()
    {
        CurrentLevelManager.instance.onLevelWon -= LevelWon;
        PlayerManager.instance.onGameOver -= ScoreManager.instance.CreateHighscoreEntry;
        PlayerManager.instance.onGameOver -= HandleGameOver;

        manager.blackscreen.StartBlackScreenFade();
        stateMachine.ChangeState(manager.stateLevelWon);
    }

    // HANDLE GAME OVER
    private void HandleGameOver()
    {
        CurrentLevelManager.instance.onLevelWon -= LevelWon;
        PlayerManager.instance.onGameOver -= ScoreManager.instance.CreateHighscoreEntry;
        PlayerManager.instance.onGameOver -= HandleGameOver;

        manager.blackscreen.StartBlackScreenFade(true, true);
        stateMachine.ChangeState(manager.stateLevelOver);
    }
}
