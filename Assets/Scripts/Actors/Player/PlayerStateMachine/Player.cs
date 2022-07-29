using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter
{
    // PLAYER DATA
    public PlayerData data;

    // STATE MACHINE
    public PlayerStateMachine stateMachine { get; private set; }

    public StatePlayerIdle stateIdle { get; private set; }
    public StatePlayerMove stateMove { get; private set; }

    // CROSSHAIR
    public Crosshair crosshair { get; private set; }

    // AWAKE
    protected override void Awake()
    {
        base.Awake();

        // PLAYER DATA
        data = new PlayerData();

        // GET MOVE INPUT COMPONENT
        moveInput = GetComponent<IMoveInput>();
        lookInput = GetComponent<ILookInput>();
        movement = GetComponent<IMoveable>();
        combat = GetComponent<ICombat>();
        
        crosshair = GameObject.Find("Crosshair").GetComponent<Crosshair>();

        // CREATE STATE MACHINE
        stateMachine = new PlayerStateMachine();
        stateIdle = new StatePlayerIdle(this, stateMachine, "idle");
        stateMove = new StatePlayerMove(this, stateMachine, "move");
    }

    // START
    protected override void Start()
    {
        stateMachine.Iinitialize(stateIdle);
    }

    // UPDATE
    protected override void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }

    // FIXED UPDATE
    protected override void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
}
