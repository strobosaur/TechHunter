using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateManagerMenuEnter : ManagerState
{
    MenuManager menu;
    GameLogoScreen gls;

    private float duration = 2f;
    private bool logoShown = false;

    // CONSTRUCTOR
    public StateManagerMenuEnter(GameManager manager, StateMachine stateMachine) : base(manager, stateMachine){}

    // ENTER
    public override void Enter()
    {
        base.Enter();

        // GET MENU COMPONENTS
        manager.menuManager = GameObject.Find("Menu").GetComponent<MenuManager>();
        menu = manager.menuManager;

        gls = GameObject.Find("GameLogo").GetComponent<GameLogoScreen>();
        gls.GetComponent<TMP_Text>().enabled = true;
        gls.LogoFadeIn();

        gls.onLogoFadeIn += LogoShown;

        // START BLACKSCREEN BLACK
        manager.blackscreen.blackscreen.color = Color.black;
        manager.blackscreen.blackscreen.enabled = true;

        // SET CAMERA STATE
        manager.cam.stateMachine.ChangeState(manager.cam.stateMenu);

        // INIT PLAYER STATS
        PlayerManager.instance.ResetGameSession();

        // LOAD GAME
        ScoreManager.instance.LoadGame();

        manager.cam.blackscreen.interactableMessage.text = "";

        manager.cam.dustPS.SetActive(true);
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

        // MAKE MENU CHOICE
        if ((logoShown) && (InputManager.input.B.WasPressedThisFrame()))
        {
            gls.GetComponent<TMP_Text>().enabled = false;
            
            AudioManager.instance.Play("menu_choice");
            stateMachine.ChangeState(manager.stateMenu);

            // START BLACKSCREEN BLACK
            manager.blackscreen.StartBlackScreenFade(false);
        }
    }

    // PHYSICS UPDATE
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // LOGO SHOWN
    public void LogoShown()
    {
        logoShown = true;
    }
}
