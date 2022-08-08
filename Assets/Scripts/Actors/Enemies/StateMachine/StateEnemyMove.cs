using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyMove : EnemyState
{
    float targetDist;
    Vector2 moveInput;
    Vector2 lookInput;

    // CONSTRUCTOR
    public StateEnemyMove(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) {}

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

        // GET INPUT
        moveInput = enemy.moveInput.GetMoveInput();
        if (lookInput != null ) lookInput = enemy.lookInput.GetLookInput();

        // DISTANCE TO TARGET
        targetDist = Vector2.Distance(enemy.transform.position, enemy.target.position);

        // CHANGE STATE?
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
        }
        else if (!(moveInput.magnitude > Mathf.Epsilon)) 
        {
            stateMachine.ChangeState(enemy.stateIdle);
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
        enemy.movement.Move(moveInput, enemy.stats.moveSpd.GetValue(), enemy.rb);
    }
}
