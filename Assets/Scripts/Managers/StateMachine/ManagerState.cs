using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerState
{
    protected GameManager manager;

    protected StateMachine stateMachine;

    protected float startTime;

    public ManagerState(GameManager manager, StateMachine stateMachine)
    {
        this.manager = manager;
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
