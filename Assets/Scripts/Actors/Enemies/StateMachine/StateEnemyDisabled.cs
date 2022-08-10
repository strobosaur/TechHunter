using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyDisabled : EnemyState
{
    float targetDist;
    Vector2 moveInput;
    Vector2 lookInput;

    // CONSTRUCTOR
    public StateEnemyDisabled(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) {}

    // DO CHECKS
    public override void DoChecks()
    {
        base.DoChecks();
    }

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
        UpdateAnimator(enemy.rb.velocity, Vector2.zero);

        // MOVE DUST
        enemy.MoveDust();
    }

    // PHYSICS UPDATE
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
