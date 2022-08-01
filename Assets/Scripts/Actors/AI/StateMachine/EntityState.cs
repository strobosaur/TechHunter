//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EntityState
//{
//    protected Movable entity;
//    protected StateMachine stateMachine;

//    protected float startTime;

//    private string animBoolName;

//    public EntityState(Movable entity, StateMachine stateMachine, string animBoolName)
//    {
//        this.entity = entity;
//        this.stateMachine = stateMachine;
//        this.animBoolName = animBoolName;
//    }

//    public virtual void Enter()
//    {
//        DoChecks();
//        startTime = Time.time;
//    }

//    public virtual void Exit(){}
    
//    public virtual void LogicUpdate(){}
    
//    public virtual void PhysicsUpdate()
//    {
//        DoChecks();
//    }
    
//    public virtual void DoChecks(){}    

//    // UPDATE ANIMATOR
//    protected virtual void UpdateAnimator(Vector2 moveDelta, Vector2 lookDelta)
//    {
//        if (entity.anim != null) 
//        {
//            // FACE MOVEMENT DIRECTION
//            if (moveDelta.magnitude > 0.2f) {
//                entity.anim.SetFloat("velX", moveDelta.x);
//                entity.anim.SetFloat("velY", moveDelta.y);

//                // UPDATE ANIMATOR PARAMETERS
//                entity.anim.SetFloat("magnitude", moveDelta.magnitude);
//                entity.anim.speed = moveDelta.magnitude * entity.data.animSpd;

//                // WEAPON
//                //if (entity.weapon != null) entity.weapon.UpdateWeapon(moveDelta);
//            } else {
//                // UPDATE ANIMATOR PARAMETERS
//                entity.anim.SetFloat("magnitude", 0f);
//                entity.anim.speed = 0f;
//            }

//            // IF HAS TARGET, FACE TARGET POSITION
//            if (lookDelta.magnitude > 0.2)
//            {
//                entity.anim.SetFloat("velX", lookDelta.x);
//                entity.anim.SetFloat("velY", lookDelta.y);

//                // WEAPON
//                //if (entity.weapon != null) entity.weapon.UpdateWeapon(lookDelta);
//            }
//        }
//    }

//    // GET FACE DIRECTION
//    protected void GetFacingDir(Vector3 target)
//    {
//        if (Vector3.Distance(target, entity.transform.position) > 0.2f)
//        {
//            // FACING DIRECTIOn
//            entity.data.facingDir = (target - entity.transform.position).normalized;
//        }
//    }
//}
