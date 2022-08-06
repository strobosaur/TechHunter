using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;

    protected float startTime;

    // CONSTRUCTOR
    public PlayerState(Player player, PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    // ON STATE ENTER
    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time;
    }

    // ON STATE EXIT
    public virtual void Exit(){}
    
    // LOGIC UPDATE
    public virtual void LogicUpdate(){}
    
    // PHYSICS UPDATE
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    
    // DO CHECKS
    public virtual void DoChecks(){}    

    // UPDATE ANIMATOR
    protected virtual void UpdateAnimator(Vector2 moveDelta, Vector2 lookDelta)
    {
        if (player.anim != null) 
        {
            // FACE MOVEMENT DIRECTION
            if (moveDelta.magnitude > 0.2f) {
                player.anim.SetFloat("velX", moveDelta.x);
                player.anim.SetFloat("velY", moveDelta.y);

                // UPDATE ANIMATOR PARAMETERS
                player.anim.SetFloat("magnitude", moveDelta.magnitude);
                player.anim.speed = moveDelta.magnitude * player.animSpd;

                // WEAPON
                if (player.weapon != null) player.combat.UpdateWeapon(moveDelta);

                // FACING
                GetFacingDir(moveDelta);
            } else {
                // UPDATE ANIMATOR PARAMETERS
                player.anim.SetFloat("magnitude", 0f);
                player.anim.speed = 0f;
            }

            // IF HAS TARGET, FACE TARGET POSITION
            if (lookDelta.magnitude > 0.2)
            {
                player.anim.SetFloat("velX", lookDelta.x);
                player.anim.SetFloat("velY", lookDelta.y);

                // WEAPON
                if (player.weapon != null) player.combat.UpdateWeapon(lookDelta);

                // FACING
                GetFacingDir(lookDelta);
            }
        }
    }

    // GET FACE DIRECTION
    protected void GetFacingDir(Vector3 target)
    {
        // FACING DIRECTIOn
        player.facingDir = target.normalized;
    }
}
