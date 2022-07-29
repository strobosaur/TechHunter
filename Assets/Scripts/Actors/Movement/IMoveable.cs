using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    public void Move(Vector2 moveInput, float moveSpd, Rigidbody2D rb);
}
