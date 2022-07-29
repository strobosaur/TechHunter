using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter
{
    protected override void Awake()
    {
        base.Awake();

        // PLAYER DATA
        data = new EntityData();

        // GET MOVE INPUT COMPONENT
        moveInput = GetComponent<IMoveInput>();
        lookInput = GetComponent<ILookInput>();
        movement = GetComponent<IMoveable>();

        // CREATE STATE MACHINE
        stateMachine = new StateMachine();
        stateIdle = new StateIdle(this, stateMachine, data, "idle");
        stateMove = new StateMove(this, stateMachine, data, "move");
    }

    protected override void Start()
    {
        stateMachine.Iinitialize(stateIdle);
    }

    protected override void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }

    protected override void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
}
