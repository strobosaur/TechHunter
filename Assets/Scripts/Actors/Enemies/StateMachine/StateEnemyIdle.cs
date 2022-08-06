using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyIdle : EnemyState
{
    float targetDist;
    Vector2 moveInput;
    Vector2 lookInput;

    // CONSTRUCTOR
    public StateEnemyIdle(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) {}

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
        base.LogicUpdate();

        // GET TARGET
        if (enemy.target == null)
            enemy.target = enemy.FindTarget();

        // DISTANCE TO TARGET
        targetDist = Vector2.Distance(enemy.transform.position, enemy.target.position);

        // GET INPUT
        moveInput = enemy.moveInput.GetMoveInput();
        lookInput = enemy.lookInput.GetLookInput();

        // CHECK FOR MOVEMENT & CHANGE STATE
        if (targetDist < enemy.stats.wpnStats.range.GetValue())
        {
            // ATTACK STATE
            stateMachine.ChangeState(enemy.stateAttack);
        }
        else if (targetDist < enemy.chargeDist)
        {
            // CHARGE STATE
            if (Time.time > enemy.timerArr[(int)EnemyTimers.chargeState] + enemy.stats.moveBoostCD.GetValue())
                stateMachine.ChangeState(enemy.stateCharge);

        } else if (moveInput.magnitude > 0) {

            // MOVE STATE
            stateMachine.ChangeState(enemy.stateMove);
        }

        // ANIMATOR
        if (moveInput.magnitude > enemy.rb.velocity.magnitude) {
            UpdateAnimator(moveInput, lookInput);
        } else {
            UpdateAnimator(enemy.rb.velocity, lookInput);
        }

        // MOVE DUST
        enemy.MoveDust();
    }

    // PHYSICS UPDATE
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
