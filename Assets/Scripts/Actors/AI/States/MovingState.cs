using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : BaseState
{
    private Vector3 moveInput;
    private MovementSM msm;

    public MovingState(MovementSM stateMachine) : base("MovingState", stateMachine) {
        msm = (MovementSM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        moveInput = Vector3.zero;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        moveInput = InputManager.input.move.ReadValue<Vector2>().normalized;
        if (moveInput.magnitude < Globals.G_LS_DEADZONE){
            stateMachine.ChangeState(msm.idleState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        msm.rb.AddForce(moveInput);
    }
}
