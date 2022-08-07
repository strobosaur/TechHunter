using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseMenuStateUpgrade : BaseMenuState
{
    int menuIndex = 0;
    List<TMP_Text> menuOptions;

    // CONSTRUCTOR
    public BaseMenuStateUpgrade(BaseManager manager, BaseMenuStateMachine stateMachine) : base(manager, stateMachine){}

    // ON STATE ENTER
    public override void Enter()
    {
        base.Enter();
        menuIndex = 0;
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
