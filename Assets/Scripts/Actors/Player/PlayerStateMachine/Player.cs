using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter
{
    // PLAYER DATA
    public PlayerData playerData;

    // STATE MACHINE
    public PlayerStateMachine stateMachine { get; private set; }

    public StatePlayerIdle stateIdle { get; private set; }
    public StatePlayerMove stateMove { get; private set; }

    // CROSSHAIR
    public Crosshair crosshair;

    protected override void Awake()
    {
        base.Awake();

        // PLAYER DATA
        playerData = new PlayerData();

        // GET MOVE INPUT COMPONENT
        moveInput = GetComponent<IMoveInput>();
        lookInput = GetComponent<ILookInput>();
        movement = GetComponent<IMoveable>();
        
        crosshair = GameObject.Find("Crosshair").GetComponent<Crosshair>();

        // CREATE STATE MACHINE
        stateMachine = new PlayerStateMachine();
        stateIdle = new StatePlayerIdle(this, stateMachine, playerData, "idle");
        stateMove = new StatePlayerMove(this, stateMachine, playerData, "move");
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
