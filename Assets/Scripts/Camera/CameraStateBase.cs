using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateBase : CameraState
{
    // CONSTRUCTOR
    public CameraStateBase(CameraController camera, CameraStateMachine stateMachine) : base(camera, stateMachine){}

    // ENTER STATE
    public override void Enter()
    {
        base.Enter();
    }

    // EXIT STATE
    public override void Exit()
    {
        base.Exit();
    }

    // LOGIC UPDATE
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    // PHYSICS UPDATE
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
