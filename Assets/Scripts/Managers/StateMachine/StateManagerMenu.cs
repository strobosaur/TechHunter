using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManagerMenu : ManagerState
{
    MenuManager menu;

    // CONSTRUCTOR
    public StateManagerMenu(GameManager manager, StateMachine stateMachine) : base(manager, stateMachine){}

    // ENTER
    public override void Enter()
    {
        base.Enter();

        // GET MENU COMPONENTS
        manager.menuManager = GameObject.Find("Menu").GetComponent<MenuManager>();
        menu = manager.menuManager;

        // START BLACKSCREEN FADE IN
        manager.blackscreen.StartBlackScreenFade(false);
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

        // SWITCH MENU INDEX
        if (InputManager.input.down.WasPressedThisFrame()) menu.currentIndex = (((menu.currentIndex + menu.menuTexts.Count) + 1) % menu.menuTexts.Count);
        if (InputManager.input.up.WasPressedThisFrame()) menu.currentIndex = (((menu.currentIndex + menu.menuTexts.Count) - 1) % menu.menuTexts.Count);

        // MAKE MENU CHOICE
        if (InputManager.input.B.WasPressedThisFrame()) 
        {
            switch (menu.currentIndex)
            {
                // NEW GAME
                case 0:
                stateMachine.ChangeState(manager.stateMenuExit);
                break;

                // HIGHSCORES
                case 1:
                break;

                // QUIT GAME
                case 2:
                Application.Quit();
                break;
            }
        }
    }

    // PHYSICS UPDATE
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
