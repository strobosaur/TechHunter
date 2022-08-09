using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateManagerMenuExit : ManagerState
{
    // MENU MANAGER
    MenuManager menu;

    // ON MENU EXIT
    public System.Action<int> OnMenuExit;

    // CONSTRUCTOR
    public StateManagerMenuExit(GameManager manager, StateMachine stateMachine) : base(manager, stateMachine){}

    // ENTER
    public override void Enter()
    {
        base.Enter();

        // GET MENU COMPONENTS
        manager.menuManager = GameObject.Find("Menu").GetComponent<MenuManager>();
        menu = manager.menuManager;

        // START BLACK SCREEN FADE
        manager.blackscreen.StartBlackScreenFade(true);
        manager.blackscreen.OnBlackScreenBlack += MakeMenuChoice;
        SceneManager.sceneLoaded += InitBaseState;
    }

    // EXIT
    public override void Exit()
    {
        base.Exit();
        SceneManager.sceneLoaded -= InitBaseState;
        manager.blackscreen.OnBlackScreenBlack -= MakeMenuChoice;
    }

    // LOGIC UPDATE
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    // PHYSICS UPDATE
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // MAKE MENU CHOICE
    private void MakeMenuChoice()
    {
        switch (menu.currentIndex)
        {
            case (int)MainMenuOptions.newGame:
            //Debug.Log("New Game!");

            // LOAD BASE SCENE
            manager.levelManager.LoadScene(manager.levelManager.sceneNames[(int)SceneName.InBase]);
            break;

            case (int)MainMenuOptions.highscores:
            break;

            case (int)MainMenuOptions.quit:
            break;
        }
    }

    public void InitBaseState(Scene s, LoadSceneMode mode)
    {
        // CHANGE STATE TO BASE STATE
        stateMachine.ChangeState(manager.stateBase);
    }
}
