using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBMovement : MonoBehaviour, IMoveable
{
    public void Move(Vector2 moveInput, float moveSpd, Rigidbody2D rb)
    {
        rb.AddForce(moveInput.normalized * moveSpd * Globals.G_MOVEFORCE);
    }
}
