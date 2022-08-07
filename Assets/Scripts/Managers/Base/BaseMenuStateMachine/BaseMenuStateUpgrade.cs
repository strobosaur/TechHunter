using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMenuStateUpgrade : BaseMenuState
{
    // CONSTRUCTOR
    public BaseMenuStateUpgrade(BaseManager manager, BaseMenuStateMachine stateMachine) : base(manager, stateMachine){}

    // ON STATE ENTER
    public override void Enter()
    {
        base.Enter();
    }

    // ON STATE EXIT
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
