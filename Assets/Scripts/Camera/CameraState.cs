using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraState
{
    protected CameraController camera;
    protected CameraStateMachine stateMachine;

    protected float startTime;

    public CameraState(CameraController camera, CameraStateMachine stateMachine)
    {
        this.camera = camera;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
    }

    public virtual void Exit(){}

    public virtual void LogicUpdate(){}

    public virtual void PhysicsUpdate(){}
}
