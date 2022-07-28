using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerMove : PlayerState
{
    public StatePlayerMove(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.moveInput.GetMoveInput().magnitude > 0)
        {
            stateMachine.ChangeState(player.stateMove);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
