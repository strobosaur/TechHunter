using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateManagerMenuHighscores : ManagerState
{
    MenuManager menu;

    // CONSTRUCTOR
    public StateManagerMenuHighscores(GameManager manager, StateMachine stateMachine) : base(manager, stateMachine){}

    // ENTER
    public override void Enter()
    {
        base.Enter();

        // DISABLE MENU TEXT
        manager.menuManager.ToggleMenuTexts(false);

        // ENABLE HIGHSCORE TEXTS
        ScoreManager.instance.DisplayHighscores(true);

        manager.cam.blackscreen.interactableMessage.text = "";
    }

    // EXIT
    public override void Exit()
    {
        base.Exit();

        // DISABLE MENU TEXT
        manager.menuManager.ToggleMenuTexts(true);

        // ENABLE HIGHSCORE TEXTS
        ScoreManager.instance.DisplayHighscores(false);
    }

    // LOGIC UPDATE
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // MAKE MENU CHOICE
        if (InputManager.input.A.WasPressedThisFrame()) 
        {
            AudioManager.instance.Play("menu_blip");
            stateMachine.ChangeState(manager.stateMenu);
        }
    }

    // PHYSICS UPDATE
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
