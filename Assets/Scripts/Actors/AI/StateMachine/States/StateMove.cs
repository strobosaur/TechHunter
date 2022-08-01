//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class StateMove : EntityState
//{
//    float targetDist;
//    Vector2 moveInput;
//    Vector2 lookInput;

//    public StateMove(Movable entity, StateMachine stateMachine, string animBoolName) : base(entity, stateMachine, animBoolName) {}

//    public override void DoChecks()
//    {
//        base.DoChecks();
//    }

//    public override void Enter()
//    {
//        base.Enter();
//    }

//    public override void Exit()
//    {
//        base.Exit();
//    }

//    public override void LogicUpdate()
//    {
//        base.LogicUpdate();

//        // GET INPUT
//        moveInput = entity.moveInput.GetMoveInput();
//        if (lookInput != null ) lookInput = entity.lookInput.GetLookInput();
        
//        // CHECK FOR MOVEMENT
//        if (moveInput.magnitude > 0) {
//        } else {
//            stateMachine.ChangeState(entity.stateMove);
//        }

//        // ANIMATOR
//        if (moveInput.magnitude > entity.rb.velocity.magnitude) {
//            UpdateAnimator(moveInput, lookInput);
//        } else {
//            UpdateAnimator(entity.rb.velocity, lookInput);
//        }

//        // MOVE DUST
//        entity.MoveDust();
//    }

//    public override void PhysicsUpdate()
//    {
//        base.PhysicsUpdate();
//        entity.movement.Move(moveInput, entity.stats.moveSpd.GetValue(), entity.rb);
//    }
//}
