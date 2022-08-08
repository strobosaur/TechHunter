using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseMenuStateMissionExit : BaseMenuState
{
    public Player player;

    // CONSTRUCTOR
    public BaseMenuStateMissionExit(BaseManager manager, BaseMenuStateMachine stateMachine) : base(manager, stateMachine){}

    // ON STATE ENTER
    public override void Enter()
    {
        base.Enter();

        player = GameManager.instance.player;

        // SET PLAYER TO DISABLED
        player.stateMachine.ChangeState(player.stateDisabled);

        // START BLACK SCREEN FADE
        GameManager.instance.blackscreen.StartBlackScreenFade(true);
        GameManager.instance.blackscreen.OnBlackScreenBlack += NextMission;
    }

    // ON STATE EXIT
    public override void Exit()
    {
        base.Exit();
    }

    // LOGIC UPDATE
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    // PHYSICS UPDATE
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // NEXT MISSION
    public void NextMission()
    {
        Debug.Log("Next Mission");
    }
}