using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movable : MonoBehaviour
{
    protected BoxCollider2D boxCollider;
    protected RaycastHit2D hit;

    protected Vector2 moveInput;
    protected Vector3 moveDelta;
    protected Vector2 movePos;
    protected Vector3 pushDirection;
    protected float pushRecoverySpeed = 0.05f;
    protected float moveSpd;
    protected float spdBoost = 1.0f;
    protected float ySpeed = 1.0f;
    protected float xSpeed = 1.0f;

    // START
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        movePos = new Vector2(0,0);
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
        hit = Physics2D.BoxCast(transform.position + new Vector3(boxCollider.offset.x,boxCollider.offset.y,0), boxCollider.size, 0, new Vector2(0,moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // MAKE ACTOR MOVE
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        } else {
            movePos.y = 0;
        }

        // COLLISION CHECK X
        hit = Physics2D.BoxCast(transform.position + new Vector3(boxCollider.offset.x,boxCollider.offset.y,0), boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // MAKE ACTOR MOVE
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        } else {
            movePos.x = 0;
        }
    }
}