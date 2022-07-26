using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSM : StateMachine
{
    public IdleState idleState;
    public MovingState movingState;
    public Rigidbody2D rb;
    
    void Awake()
    {
        idleState = new IdleState(this);
        movingState = new MovingState(this);
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}
