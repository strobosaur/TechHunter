using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime;

    private string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        //player.anim.SetBool(animBoolName, true);
        startTime = Time.time;
    }

    public virtual void Exit()
    {
        //player.anim.SetBool(animBoolName, false);
    }
    
    public virtual void LogicUpdate(){}
    
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    
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
                player.anim.speed = moveDelta.magnitude * playerData.animSpd;
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
            }
        }
    }

    // GET FACE DIRECTION
    protected void GetFacingDir(Vector3 target)
    {
        if (Vector3.Distance(target, player.transform.position) > 0.2f)
        {
            // FACING DIRECTIOn
            playerData.facingDir = (target - player.transform.position).normalized;
        }
    }
}
