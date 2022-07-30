using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyCharge : EnemyState
{
    float targetDist;
    Vector2 moveInput;
    Vector2 lookInput;

    // CONSTRUCTOR
    public StateEnemyCharge(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName) {}

    // DO CHECKS
    public override void DoChecks()
    {
        base.DoChecks();
    }

    // ON STATE ENTER
    public override void Enter()
    {
        base.Enter();
        enemy.stats.moveSpd.AddModifier(enemy.stats.moveBoost.GetValue());
    }

    // ON STATE EXIT
    public override void Exit()
    {
        base.Exit();
        enemy.stats.moveSpd.RemoveModifier(enemy.stats.moveBoost.GetValue());
    }

    // LOGIC UPDATE
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // GET TARGET
        if (enemy.target == null)
        {
            enemy.target = enemy.FindTarget();
            stateMachine.ChangeState(enemy.stateIdle);
        }

        // DISTANCE TO TARGET
        moveInput = (enemy.target.position - enemy.transform.position).normalized;

        // DISTANCE TO TARGET
        targetDist = Vector2.Distance(enemy.transform.position, enemy.target.position);

        // SWITCH STATES
        if (targetDist <= enemy.weapon.GetWeaponParams().range.GetValue()) {
            stateMachine.ChangeState(enemy.stateAttack);
        } else if (Time.time > startTime + enemy.stats.moveBoostTime.GetValue()) {
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
