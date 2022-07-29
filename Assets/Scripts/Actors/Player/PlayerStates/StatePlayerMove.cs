using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerMove : PlayerState
{
    Vector2 moveInput;
    Vector2 lookInput;

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

        // GET INPUT
        moveInput = player.moveInput.GetMoveInput();
        lookInput = player.lookInput.GetLookInput();

        // CROSSHAIR
        player.crosshair.UpdateCrosshair(lookInput);
        
        // CHECK FOR MOVEMENT
        if (moveInput.magnitude > 0) {
            player.movement.Move(moveInput, player.playerData.moveSpd, player.rb);
        } else {
            stateMachine.ChangeState(player.stateMove);
        }

        // ANIMATOR
        if (moveInput.magnitude > player.rb.velocity.magnitude) {
            UpdateAnimator(moveInput, lookInput);
        } else {
            UpdateAnimator(player.rb.velocity, lookInput);
        }

        // MOVE DUST
        player.MoveDust();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
