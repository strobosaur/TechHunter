using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;

    protected float startTime;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time;
    }

    public virtual void Exit(){}
    
    public virtual void LogicUpdate(){}
    
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    
    public virtual void DoChecks(){}    

    // UPDATE ANIMATOR
    protected virtual void UpdateAnimator(Vector2 moveDelta, Vector2 lookDelta)
    {
        if (enemy.anim != null) 
        {
            // FACE MOVEMENT DIRECTION
            if (moveDelta.magnitude > 0.2f) {
                enemy.anim.SetFloat("velX", moveDelta.x);
                enemy.anim.SetFloat("velY", moveDelta.y);

                // UPDATE ANIMATOR PARAMETERS
                enemy.anim.SetFloat("magnitude", moveDelta.magnitude);
                enemy.anim.speed = moveDelta.magnitude * enemy.animSpd;

                // WEAPON
                if (enemy.weapon != null) enemy.combat.UpdateWeapon(moveDelta);
            } else {
                // UPDATE ANIMATOR PARAMETERS
                enemy.anim.SetFloat("magnitude", 0f);
                enemy.anim.speed = 0f;
            }

            // IF HAS TARGET, FACE TARGET POSITION
            if (lookDelta.magnitude > 0.2)
            {
                enemy.anim.SetFloat("velX", lookDelta.x);
                enemy.anim.SetFloat("velY", lookDelta.y);

                // WEAPON
                if (enemy.weapon != null) enemy.combat.UpdateWeapon(lookDelta);
            }
        }
    }

    // GET FACE DIRECTION
    protected void GetFacingDir(Vector3 target)
    {
        if (Vector3.Distance(target, enemy.transform.position) > 0.2f)
        {
            // FACING DIRECTIOn
            enemy.facingDir = (target - enemy.transform.position).normalized;
        }
    }
}
