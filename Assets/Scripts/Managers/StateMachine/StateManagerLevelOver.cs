using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManagerLevelOver : ManagerState
{
    public StateManagerLevelOver(GameManager manager, StateMachine stateMachine) : base(manager, stateMachine){}

    bool blackScreenBlack = false;
    bool hsDisplayed = false;

    // ON STATE ENTER
    public override void Enter()
    {
        base.Enter();

        blackScreenBlack = false;
        hsDisplayed = false;

        manager.blackscreen.OnBlackScreenBlack += ToggleInteraction;
    }

    // ON STATE EXIT
    public override void Exit()
    {
        base.Exit();

        // RESET GAME SESSION
        PlayerManager.instance.ResetGameSession();

        manager.blackscreen.StartBlackScreenFade(false);

        // UNSUBSCRIBE        
        manager.blackscreen.OnBlackScreenBlack -= ToggleInteraction;
    }

    // STATE LOGIC UPDATE
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if ((blackScreenBlack) && (!hsDisplayed) && (InputManager.input.B.WasPressedThisFrame())) {
            ScoreManager.instance.DisplayHighscores();
            hsDisplayed = true;
        } else if ((hsDisplayed) && (InputManager.input.B.WasPressedThisFrame())) {
            ScoreManager.instance.DisplayHighscores(false);

            // CLEAR LEVEL OBJECTS
            PlayerManager.instance.playerList.Clear();
            PropGenerator.instance.ClearAllProps();
            EnemyManager.instance.spawnPointGenerator.DeleteAllSpawnPoints();
            EnemyManager.instance.ClearAllEnemies();

            // LOAD BASE SCENE
            manager.levelManager.LoadScene(manager.levelManager.sceneNames[(int)SceneName.MainMenu]);
        }
    }

    // STATE PHYSICS UPDATE
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // TOGGLE INTERACTABLE
    private void ToggleInteraction()
    {
        blackScreenBlack = true;
    }
}
