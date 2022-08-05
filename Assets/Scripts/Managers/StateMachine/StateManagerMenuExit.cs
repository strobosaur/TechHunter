using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            Debug.Log("New Game!");
            stateMachine.ChangeState(manager.stateBase);
            break;

            case (int)MainMenuOptions.highscores:
            break;

            case (int)MainMenuOptions.quit:
            break;
        }
    }
}
