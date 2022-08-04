using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManagerMenu : ManagerState
{
    MenuManager menu;

    public StateManagerMenu(GameManager manager, StateMachine stateMachine) : base(manager, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        manager.menuManager = GameObject.Find("Menu").GetComponent<MenuManager>();
        menu = manager.menuManager;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (InputManager.input.down.WasPressedThisFrame()) menu.currentIndex = (((menu.currentIndex + menu.menuTexts.Count) + 1) % menu.menuTexts.Count);
        if (InputManager.input.up.WasPressedThisFrame()) menu.currentIndex = (((menu.currentIndex + menu.menuTexts.Count) - 1) % menu.menuTexts.Count);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
