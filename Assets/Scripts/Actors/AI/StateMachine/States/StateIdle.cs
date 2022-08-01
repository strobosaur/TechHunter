//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class StateIdle : EntityState
//{
//    float targetDist;
//    Vector2 moveInput;
//    Vector2 lookInput;

//    // CONSTRUCTOR
//    public StateIdle(Movable entity, StateMachine stateMachine, string animBoolName) : base(entity, stateMachine, animBoolName)
//    {
//    }

//    // DO CHECKS
//    public override void DoChecks()
//    {
//        base.DoChecks();
//    }

//    // ON STATE ENTER
//    public override void Enter()
//    {
//        base.Enter();
//    }

//    // ON STATE EXIT
//    public override void Exit()
//    {
//        base.Exit();
//    }

//    // LOGIC UPDATE
//    public override void LogicUpdate()
//    {
//        base.LogicUpdate();

//        // GET INPUT
//        moveInput = entity.moveInput.GetMoveInput();
//        lookInput = entity.lookInput.GetLookInput();

//        // CHECK FOR MOVEMENT & CHANGE STATE
//        if (moveInput.magnitude > 0)
//        {
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

//    // PHYSICS UPDATE
//    public override void PhysicsUpdate()
//    {
//        base.PhysicsUpdate();
//    }
//}
