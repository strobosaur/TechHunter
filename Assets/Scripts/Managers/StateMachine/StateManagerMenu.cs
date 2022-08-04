using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManagerMenu : ManagerState
{
    MenuManager menu;

    Image blackscreen;
    Color bsColor;

    float bsFadeTime = 2f;
    float bsFadeCounter = 0f;

    // CONSTRUCTOR
    public StateManagerMenu(GameManager manager, StateMachine stateMachine) : base(manager, stateMachine){}

    // ENTER
    public override void Enter()
    {
        base.Enter();

        manager.menuManager = GameObject.Find("Menu").GetComponent<MenuManager>();
        menu = manager.menuManager;

        blackscreen = GameObject.Find("BlackScreen").GetComponent<Image>();
        bsColor = blackscreen.color;
        bsColor.a = 1f;
        blackscreen.color = bsColor;
        blackscreen.enabled = true;
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

        // UPDATE BLACKSCREEN
        BlackScreenFade(blackscreen, false);

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

    // BLACKSCREEN FADE
    void BlackScreenFade(Image blackscreen, bool fadeIn)
    {
        if (bsFadeCounter < bsFadeTime)
        {
            bsFadeCounter = Globals.Approach(bsFadeCounter, bsFadeTime, Time.deltaTime);
            if (fadeIn) bsColor.a = (bsFadeCounter / bsFadeTime);
            else bsColor.a = 1f - (bsFadeCounter / bsFadeTime);
            blackscreen.color = bsColor;
        } else {
            if (!fadeIn)
                blackscreen.enabled = false;
        }
    }
}
