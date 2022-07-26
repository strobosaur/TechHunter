using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerIdle : PlayerState
{
    Vector2 moveInput;
    Vector2 lookInput;

    public StatePlayerIdle(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
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

        // MOVE BOOST
        if (InputManager.input.L2.IsPressed()) {
            player.moveBoost.MoveBoost();
        }

        // CHECK FOR MOVEMENT & CHANGE STATE
        if (moveInput.magnitude > Mathf.Epsilon)
        {
            stateMachine.ChangeState(player.stateMove);
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

        // COMBAT
        if (InputManager.input.R2.IsPressed())
            player.combat.Attack(player.weapon, player.rb.position, player.crosshair.transform, player.stats.wpnStats);

        // INTERACTION
        player.interaction.UpdateInteractable();
        if (InputManager.input.B.WasPressedThisFrame()) {
            player.interaction.InteractClosest();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
