using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMenuState
{
    protected BaseMenuStateMachine stateMachine;
    protected BaseManager manager;

    protected float startTime;

    public BaseMenuState(BaseManager manager, BaseMenuStateMachine stateMachine) {
        this.stateMachine = stateMachine;
        this.manager = manager;
    }

    public virtual void Enter(){
        startTime = Time.time;
    }

    public virtual void Exit(){}

    public virtual void LogicUpdate(){}

    public virtual void PhysicsUpdate(){}
}
