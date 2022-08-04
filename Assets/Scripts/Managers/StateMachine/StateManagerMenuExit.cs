using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManagerMenuExit : ManagerState
{
    MenuManager menu;

    Image blackscreen;
    Color bsColor;

    float bsFadeTime = 2f;
    float bsFadeCounter = 0;

    // CONSTRUCTOR
    public StateManagerMenuExit(GameManager manager, StateMachine stateMachine) : base(manager, stateMachine){}

    // ENTER
    public override void Enter()
    {
        base.Enter();
        manager.menuManager = GameObject.Find("Menu").GetComponent<MenuManager>();
        menu = manager.menuManager;
        
        blackscreen = GameObject.Find("BlackScreen").GetComponent<Image>();
        bsColor = blackscreen.color;
        bsColor.a = 0;
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

        BlackScreenFade(blackscreen, true);
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
