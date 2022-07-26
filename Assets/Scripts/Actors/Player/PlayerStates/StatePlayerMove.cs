using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerMove : PlayerState
{
    Vector2 moveInput;
    Vector2 lookInput;

    public StatePlayerMove(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine){}

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
            player.combat.Attack(player.weapon, player.rb.position, player.crosshair.transform, player.stats.wpnStats);

        // MOVE BOOST
        if (InputManager.input.L2.IsPressed()) {
            player.moveBoost.MoveBoost();
        }

        // INVINCIBILITY UPDATE
        player.invincibility.UpdateInvincibilityTimer();

        // ANIMATOR
        if (moveInput.magnitude > player.rb.velocity.magnitude) {
            UpdateAnimator(moveInput, lookInput);
        } else {
            UpdateAnimator(player.rb.velocity, lookInput);
        }

        // MOVE DUST
        player.MoveDust();
        
        // CHECK FOR MOVEMENT
        if (!(moveInput.magnitude > Mathf.Epsilon)) {
            stateMachine.ChangeState(player.stateIdle);            
        }

        // INTERACTION
        player.interaction.UpdateInteractable();
        if (InputManager.input.B.WasPressedThisFrame()) {
            player.interaction.InteractClosest();
        }
    }

    // PHYSICS UPDATE
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.movement.Move(moveInput, player.stats.moveSpd.GetValue(), player.rb);
    }
}
