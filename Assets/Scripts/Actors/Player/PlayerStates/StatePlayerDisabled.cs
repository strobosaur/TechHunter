using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerDisabled : PlayerState
{
    Vector2 moveInput;
    Vector2 lookInput;

    public StatePlayerDisabled(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

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

        // ANIMATOR
        UpdateAnimator(player.rb.velocity, Vector2.zero);

        // MOVE DUST
        player.MoveDust();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
