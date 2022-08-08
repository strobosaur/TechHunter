using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseMenuStateMission : BaseMenuState
{
    public Player player;
    List<TMP_Text> menuOptions;
    private bool isInteractable;

    public System.Action onMenuIndexChanged;

    int menuIndex = 0;

    // CONSTRUCTOR
    public BaseMenuStateMission(BaseManager manager, BaseMenuStateMachine stateMachine) : base(manager, stateMachine){}

    // ON STATE ENTER
    public override void Enter()
    {
        base.Enter();

        player = GameManager.instance.player;
        isInteractable = false;

        // SET PLAYER TO DISABLED
        player.stateMachine.ChangeState(player.stateDisabled);

        menuIndex = 0;

        // GET TEXT REFERENCES
        menuOptions = manager.missionManager.missionTextLocList;

        // SUBSCRIBE TO EVENTS
        onMenuIndexChanged += UpdateMenuTexts;

        // ENABLE GUI GROUPS
        manager.TogglePlayerMenu(false);
        manager.ToggleUpgradeMenu(false);

        // UPDATE TEXTS
        UpdateMenuTexts();
    }

    // ON STATE EXIT
    public override void Exit()
    {
        base.Exit();

        // UNSUBSCRIBE TO EVENTS
        onMenuIndexChanged -= UpdateMenuTexts;

        // DISABLE GUI GROUPS
        
        // SET PLAYER TO ACTIVE
        player.stateMachine.ChangeState(player.stateIdle);
    }

    // LOGIC UPDATE
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isInteractable)
        {
            // SWITCH MENU INDEX
            if (InputManager.input.down.WasPressedThisFrame()) 
            {
                menuIndex = (((menuIndex + menuOptions.Count) + 1) % menuOptions.Count);
                onMenuIndexChanged?.Invoke();
            }

            if (InputManager.input.up.WasPressedThisFrame()) 
            {
                menuIndex = (((menuIndex + menuOptions.Count) - 1) % menuOptions.Count);
                onMenuIndexChanged?.Invoke();
            }

            // MAKE MENU CHOICE
            if (InputManager.input.B.WasPressedThisFrame()) 
            {
                switch (menuIndex)
                {
                    // UPGRADE WEAPON
                    case ((int)UpgradeType.weapon):
                    manager.upgManager.HandleUpgrade(PlayerManager.instance.playerStats, UpgradeType.weapon, 1);
                    break;

                    // UPGRADE ARMOR
                    case ((int)UpgradeType.armor):
                    manager.upgManager.HandleUpgrade(PlayerManager.instance.playerStats, UpgradeType.armor, 1);
                    break;

                    // UPGRADE HEAD
                    case ((int)UpgradeType.head):
                    manager.upgManager.HandleUpgrade(PlayerManager.instance.playerStats, UpgradeType.head, 1);
                    break;

                    // UPGRADE BOOTS
                    case ((int)UpgradeType.boots):
                    manager.upgManager.HandleUpgrade(PlayerManager.instance.playerStats, UpgradeType.boots, 1);
                    break;

                    // UPGRADE BODY
                    case ((int)UpgradeType.body):
                    manager.upgManager.HandleUpgrade(PlayerManager.instance.playerStats, UpgradeType.body, 1);
                    break;
                }
            }

            // EXIT MENU
            if (InputManager.input.A.WasPressedThisFrame()){
                stateMachine.ChangeState(manager.stateIdle);
            }
        } else {
            isInteractable = true;
        }
    }

    // PHYSICS UPDATE
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // UPDATE UPGRADE TEXTS
    public void UpdateMenuTexts()
    {
        for (int i = 0; i < menuOptions.Count; i++)
        {
            if (i == menuIndex) {
                menuOptions[i].GetComponent<MenuText>().isChosen = true;
            } else {
                menuOptions[i].GetComponent<MenuText>().isChosen = false;
            }
        }
    }
}
