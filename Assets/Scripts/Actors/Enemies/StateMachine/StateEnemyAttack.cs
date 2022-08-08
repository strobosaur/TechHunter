using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyAttack : EnemyState
{
    float targetDist;
    Vector2 moveInput;
    Vector2 lookInput;

    // CONSTRUCTOR
    public StateEnemyAttack(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) {}

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
        {
            enemy.target = enemy.FindTarget();
            stateMachine.ChangeState(enemy.stateIdle);
            return;
        }

        // DISTANCE TO TARGET
        targetDist = Vector2.Distance(enemy.transform.position, enemy.target.position);

        // SWITCH STATES
        if (targetDist > enemy.wpnStats.range.GetValue())
        {
            // TARGET OUT OF RANGE = BACK TO IDLE STATE
            stateMachine.ChangeState(enemy.stateIdle);
        } else {

            // IF IN RANGE; ATTACK ENEMY
            enemy.combat.Attack(enemy.weapon, enemy.transform.position, enemy.target, enemy.stats.wpnStats);
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
