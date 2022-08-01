using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerMove : PlayerState
{
    Vector2 moveInput;
    Vector2 lookInput;

    public StatePlayerMove(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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

    // LOGIC UPDATE
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // GET INPUT
        moveInput = player.moveInput.GetMoveInput();
        lookInput = player.lookInput.GetLookInput();

        // CROSSHAIR
        player.crosshair.UpdateCrosshair(lookInput);

        // COMBAT
        if (InputManager.input.R2.IsPressed())
            player.combat.Attack(player.weapon, player.rb.position, player.crosshair.transform);

        // MOVE BOOST
        if (InputManager.input.B.IsPressed()) {
            player.moveBoost.MoveBoost();
        }
        
        // CHECK FOR MOVEMENT
        if (moveInput.magnitude > 0) {
            
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

    // PHYSICS UPDATE
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.movement.Move(moveInput, player.stats.moveSpd.GetValue(), player.rb);
    }
}
