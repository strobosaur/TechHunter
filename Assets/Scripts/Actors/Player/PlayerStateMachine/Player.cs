using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine stateMachine { get; private set; }

    public Animator anim { get; private set; }

    public PlayerData playerData;

    public StatePlayerIdle stateIdle { get; private set; }
    public StatePlayerMove stateMove { get; private set; }

    void Awake()
    {
        stateMachine = new PlayerStateMachine();
        stateIdle = new StatePlayerIdle(this, stateMachine, playerData, "idle");
        stateMove = new StatePlayerMove(this, stateMachine, playerData, "move");
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        stateMachine.Iinitialize(stateIdle);
    }

    void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
}
