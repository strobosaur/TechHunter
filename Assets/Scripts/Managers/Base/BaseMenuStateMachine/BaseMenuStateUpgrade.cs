using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseMenuStateUpgrade : BaseMenuState
{
    public Player player;
    List<TMP_Text> menuOptions;
    List<TMP_Text> menuCostsTech;
    List<TMP_Text> menuCostsScraps;

    public System.Action onMenuIndexChanged;

    int menuIndex = 0;

    // CONSTRUCTOR
    public BaseMenuStateUpgrade(BaseManager manager, BaseMenuStateMachine stateMachine) : base(manager, stateMachine){}

    // ON STATE ENTER
    public override void Enter()
    {
        base.Enter();

        player = GameManager.instance.player;

        // SET PLAYER TO DISABLED
        player.stateMachine.ChangeState(player.stateDisabled);

        menuIndex = 0;

        // GET TEXT REFERENCES
        menuOptions = manager.wpnUpgTexts;
        menuCostsTech = manager.wpnUpgFieldsTech;
        menuCostsScraps = manager.wpnUpgFieldsScraps;

        // SUBSCRIBE TO EVENTS
        manager.upgManager.onStatsChanged += manager.UpdatePlayerTexts;
        manager.upgManager.onStatsChanged += UpdateCostTexts;
        onMenuIndexChanged += UpdateMenuTexts;

        Inventory.instance.onScrapsChanged += manager.UpdateResourceTexts;
        Inventory.instance.onTechChanged += manager.UpdateResourceTexts;

        // ENABLE GUI GROUPS
        manager.TogglePlayerMenu(true);
        manager.ToggleUpgradeMenu(true);

        // UPDATE TEXTS
        manager.UpdatePlayerTexts();
        UpdateMenuTexts();
        UpdateCostTexts();
    }

    // ON STATE EXIT
    public override void Exit()
    {
        base.Exit();

        // UNSUBSCRIBE TO EVENTS
        manager.upgManager.onStatsChanged -= manager.UpdatePlayerTexts;
        manager.upgManager.onStatsChanged -= UpdateCostTexts;
        onMenuIndexChanged -= UpdateMenuTexts;

        Inventory.instance.onScrapsChanged -= manager.UpdateResourceTexts;
        Inventory.instance.onTechChanged -= manager.UpdateResourceTexts;

        // DISABLE GUI GROUPS
        manager.TogglePlayerMenu(false);
        manager.ToggleUpgradeMenu(false);
        
        // SET PLAYER TO ACTIVE
        player.stateMachine.ChangeState(player.stateIdle);
    }

    // LOGIC UPDATE
    public override void LogicUpdate()
    {
        base.LogicUpdate();

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

        // CHEAT MONEY
        if (InputManager.input.X.WasPressedThisFrame()){
            Inventory.instance.ChangeScraps(100);
            Inventory.instance.ChangeTechUnits(1);
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
                menuCostsTech[i].GetComponent<MenuText>().isChosen = true;
                menuCostsScraps[i].GetComponent<MenuText>().isChosen = true;
            } else {
                menuOptions[i].GetComponent<MenuText>().isChosen = false;
                menuCostsTech[i].GetComponent<MenuText>().isChosen = false;
                menuCostsScraps[i].GetComponent<MenuText>().isChosen = false;
            }
        }
    }

    // UPDATE COST TEXTS
    public void UpdateCostTexts()
    {
        for (int i = 0; i < menuCostsTech.Count; i++)
        {
            menuCostsTech[i].text = manager.upgManager.upgradeCostsTech[i].ToString();
            menuCostsScraps[i].text = manager.upgManager.upgradeCostsScraps[i].ToString();
        }
    }
}
