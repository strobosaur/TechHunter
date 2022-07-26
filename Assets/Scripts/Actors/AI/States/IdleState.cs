using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private Vector3 moveInput;
    private MovementSM msm;

    public IdleState(MovementSM stateMachine) : base("IdleState", stateMachine) {
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
        if (moveInput.magnitude > Globals.G_LS_DEADZONE){
            stateMachine.ChangeState(msm.movingState);
        }
    }
}
