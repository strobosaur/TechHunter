using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : EntityState
{
    Vector2 moveInput;
    Vector2 lookInput;

    public StateIdle(Movable entity, StateMachine stateMachine, EntityData data, string animBoolName) : base(entity, stateMachine, data, animBoolName)
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
        moveInput = entity.moveInput.GetMoveInput();
        lookInput = entity.lookInput.GetLookInput();

        // CHECK FOR MOVEMENT & CHANGE STATE
        if (moveInput.magnitude > 0)
        {
            stateMachine.ChangeState(entity.stateMove);
        }

        // ANIMATOR
        if (moveInput.magnitude > entity.rb.velocity.magnitude) {
            UpdateAnimator(moveInput, lookInput);
        } else {
            UpdateAnimator(entity.rb.velocity, lookInput);
        }

        // MOVE DUST
        entity.MoveDust();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
