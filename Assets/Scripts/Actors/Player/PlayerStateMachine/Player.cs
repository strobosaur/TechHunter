using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ANIMATOR
    public Animator anim { get; private set; }

    // RIGIDBODY
    public Rigidbody2D rb { get; private set; }

    // PLAYER DATA
    public PlayerData playerData;

    // STATE MACHINE
    public PlayerStateMachine stateMachine { get; private set; }

    public StatePlayerIdle stateIdle { get; private set; }
    public StatePlayerMove stateMove { get; private set; }

    // INPUT
    public IMoveInput moveInput;
    public ILookInput lookInput;

    // PARTICLE SYSTEMS
    public ParticleSystem dustPS;

    // CROSSHAIR
    private Crosshair crosshair;

    void Awake()
    {
        // CREATE STATE MACHINE
        stateMachine = new PlayerStateMachine();
        stateIdle = new StatePlayerIdle(this, stateMachine, playerData, "idle");
        stateMove = new StatePlayerMove(this, stateMachine, playerData, "move");

        // GET MOVE INPUT COMPONENT
        moveInput = GetComponent<IMoveInput>();
        lookInput = GetComponent<ILookInput>();
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

    // MOVE DUST
    public void MoveDust()
    {
        // DUST PS
        dustPS.transform.position = transform.position - ((Vector3)rb.velocity * 0.2f);
        if (rb.velocity.magnitude > 2f){
            if (!dustPS.isEmitting) {
                dustPS.Play();
            }
        } else dustPS.Stop();
    }
}
