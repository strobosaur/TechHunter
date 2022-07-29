using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    // ANIMATOR
    public Animator anim { get; protected set; }

    // RIGIDBODY
    public Rigidbody2D rb { get; protected set; }

    // PLAYER DATA
    public EntityData data;

    // STATE MACHINE
    public StateMachine stateMachine { get; protected set; }

    public StateIdle stateIdle { get; protected set; }
    public StateMove stateMove { get; protected set; }

    // MOVEMENT
    public IMoveInput moveInput;
    public ILookInput lookInput;
    public IMoveable movement;
    public ICombat combat;

    public IWeapon weapon;

    public Queue<Vector2> forceQueue;

    // PARTICLE SYSTEMS
    public ParticleSystem dustPS;

    protected virtual void Awake()
    {
        // COMPONENTS
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // PLAYER DATA
        data = new EntityData();

        // FORCE QUEUE
        forceQueue = new Queue<Vector2>();

        // GET MOVE INPUT COMPONENT
        moveInput = GetComponent<IMoveInput>();
        lookInput = GetComponent<ILookInput>();
        movement = GetComponent<IMoveable>();
        combat = GetComponent<ICombat>();

        // CREATE STATE MACHINE
        stateMachine = new StateMachine();
        stateIdle = new StateIdle(this, stateMachine, "idle");
        stateMove = new StateMove(this, stateMachine, "move");
    }

    // START
    protected virtual void Start()
    {
        stateMachine.Iinitialize(stateIdle);
    }

    // UPDATE
    protected virtual void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }

    // FIXED UPDATE
    protected virtual void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
        if (forceQueue.Count > 0) ApplyForces();
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

    // APPLY FORCES
    private void ApplyForces()
    {
        while (forceQueue.Count > 0)
        {
            rb.AddForce(forceQueue.Dequeue());
        }
    }
}
