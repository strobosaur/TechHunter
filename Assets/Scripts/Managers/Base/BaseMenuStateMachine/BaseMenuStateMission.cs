using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseMenuStateMission : BaseMenuState
{
    public Player player;
    List<TMP_Text> menuOptions;
    List<TMP_Text> menuMapDifficulties;
    List<TMP_Text> menuMapSizes;
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
        menuMapDifficulties = manager.missionManager.missionTextDifList;
        menuMapSizes = manager.missionManager.missionTextSizeList;

        // SUBSCRIBE TO EVENTS
        onMenuIndexChanged += UpdateMenuTexts;

        // ENABLE GUI GROUPS
        manager.missionManager.ToggleMissionMenu(true);

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
        manager.missionManager.ToggleMissionMenu(false);
        
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

                // PLAY SOUND EFFECT
                AudioManager.instance.Play("menu_blip");
            }

            if (InputManager.input.up.WasPressedThisFrame()) 
            {
                menuIndex = (((menuIndex + menuOptions.Count) - 1) % menuOptions.Count);
                onMenuIndexChanged?.Invoke();

                // PLAY SOUND EFFECT
                AudioManager.instance.Play("menu_blip");
            }

            // MAKE MENU CHOICE
            if (InputManager.input.A.WasPressedThisFrame()){

                // PLAY SOUND EFFECT
                AudioManager.instance.Play("menu_blip");

                // EXIT MENU
                stateMachine.ChangeState(manager.stateIdle);

            } else if (InputManager.input.B.WasPressedThisFrame()) {

                // PLAY SOUND EFFECT
                AudioManager.instance.Play("menu_choice");
                
                // NEXT MISSION
                GameManager.instance.levelManager.SetNextLevel(manager.missionManager.missionList[menuIndex]);
                stateMachine.ChangeState(manager.stateMissionExit);
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
                menuMapDifficulties[i].GetComponent<MenuText>().isChosen = true;
                menuMapSizes[i].GetComponent<MenuText>().isChosen = true;
            } else {
                menuOptions[i].GetComponent<MenuText>().isChosen = false;
                menuMapDifficulties[i].GetComponent<MenuText>().isChosen = false;
                menuMapSizes[i].GetComponent<MenuText>().isChosen = false;
            }
        }
    }
}
