using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseMenuStateUpgrade : BaseMenuState
{
    public Player player;
    List<TMP_Text> menuOptions;

    public System.Action onMenuIndexChanged;

    int menuIndex = 0;

    // CONSTRUCTOR
    public BaseMenuStateUpgrade(BaseManager manager, BaseMenuStateMachine stateMachine) : base(manager, stateMachine){}

    // ON STATE ENTER
    public override void Enter()
    {
        base.Enter();

        player = GameManager.instance.player;
        player.stateMachine.ChangeState(player.stateDisabled);

        menuIndex = 0;
        menuOptions = manager.wpnUpgTexts;
        manager.upgManager.onStatsChanged += manager.UpdatePlayerTexts;
        onMenuIndexChanged += UpdateMenuTexts;
        UpdateMenuTexts();
    }

    // ON STATE EXIT
    public override void Exit()
    {
        base.Exit();

        manager.upgManager.onStatsChanged -= manager.UpdatePlayerTexts;
        onMenuIndexChanged -= UpdateMenuTexts;
        
        player.stateMachine.ChangeState(player.stateIdle);
    }

    // LOGIC UPDATE
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // SWITCH MENU INDEX
        if (InputManager.input.down.WasPressedThisFrame()) 
        {
            Debug.Log(menuIndex);
            menuIndex = (((menuIndex + menuOptions.Count) + 1) % menuOptions.Count);
            onMenuIndexChanged?.Invoke();
        }

        if (InputManager.input.up.WasPressedThisFrame()) 
        {
            Debug.Log(menuIndex);
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
                manager.upgManager.UpgradeWeapon(PlayerManager.instance.playerStats.wpnStats, 1);
                break;

                // UPGRADE ARMOR
                case ((int)UpgradeType.armor):
                //upgManager.UpgradeWeapon(PlayerManager.instance.playerStats.wpnStats, 1);
                break;

                // UPGRADE ARMOR
                case ((int)UpgradeType.helmet):
                //upgManager.UpgradeWeapon(PlayerManager.instance.playerStats.wpnStats, 1);
                break;

                // UPGRADE ARMOR
                case ((int)UpgradeType.boots):
                //upgManager.UpgradeWeapon(PlayerManager.instance.playerStats.wpnStats, 1);
                break;

                // UPGRADE ARMOR
                case ((int)UpgradeType.body):
                //upgManager.UpgradeWeapon(PlayerManager.instance.playerStats.wpnStats, 1);
                break;
            }

            //manager.UpdatePlayerTexts();
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
