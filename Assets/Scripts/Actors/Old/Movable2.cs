using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movable2 : MonoBehaviour
{
    // ANIMATOR
    protected Animator anim;
    protected float animSpd = 1f;

    // COLLISIONS
    protected CircleCollider2D circleCollider;
    protected RaycastHit2D hit;

    // MOVE PARAMETERS
    protected Vector2 moveInput;
    protected Vector3 moveDelta;
    protected Vector2 movePos;
    protected Vector3 facingDir;
    protected Vector3 pushDirection;
    protected float pushRecoverySpeed = 0.05f;
    protected float moveSpd;
    protected float spdBoost = 1.0f;
    protected float ySpeed = 1.0f;
    protected float xSpeed = 1.0f;

    // START
    protected virtual void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        movePos = new Vector2(0,0);
    }

    // UPDATE
    protected virtual void Update()
    {
        GetFacingDir(moveDelta);
        UpdateAnimator();
    }

    // FIXED UPDATE
    protected virtual void FixedUpdate()
    {
        UpdateMotor(moveInput);
    }

    // MOVEMENT
    protected virtual void UpdateMotor(Vector3 input)
    {        
        // RESET MOVE DELTA
        moveInput = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);
        movePos = Vector2.Lerp(movePos, moveInput * moveSpd * spdBoost, Globals.G_INERTIA);
        moveDelta = new Vector3(movePos.x, movePos.y, 0);

        // ADD PUSH VECTOR IF ANY
        moveDelta += pushDirection;

        // REDUCE PUSH FORCE
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        // COLLISION CHECK Y
        hit = Physics2D.CircleCast(transform.position + new Vector3(circleCollider.offset.x,circleCollider.offset.y,0), circleCollider.radius, new Vector2(0,moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // MAKE ACTOR MOVE
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        } else {
            movePos.y = 0;
        }

        // COLLISION CHECK X
        hit = Physics2D.CircleCast(transform.position + new Vector3(circleCollider.offset.x,circleCollider.offset.y,0), circleCollider.radius, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // MAKE ACTOR MOVE
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        } else {
            movePos.x = 0;
        }
    }

    // UPDATE ANIMATOR
    protected virtual void UpdateAnimator()
    {
        if (anim != null) 
        {
            // FACE MOVEMENT DIRECTION
            if (moveDelta.magnitude > 0.1f) {
                anim.SetFloat("velX", moveDelta.x);
                anim.SetFloat("velY", moveDelta.y);

                // UPDATE ANIMATOR PARAMETERS
                anim.SetFloat("magnitude", moveDelta.magnitude);
                anim.speed = moveDelta.magnitude * animSpd;
            } else {
                // UPDATE ANIMATOR PARAMETERS
                anim.SetFloat("magnitude", 0f);
                anim.speed = 0f;
            }

            // IF HAS TARGET, FACE TARGET POSITION
            if (facingDir.magnitude > 0.2)
            {
                anim.SetFloat("velX", facingDir.x);
                anim.SetFloat("velY", facingDir.y);
            }
        }
    }

    // GET FACE DIRECTION
    protected void GetFacingDir(Vector3 target)
    {
        if (Vector3.Distance(target, transform.position) > 0.2f)
        {
            // FACING DIRECTIOn
            facingDir = (target - transform.position).normalized;
        }
    }
}