using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    // ANIMATOR
    public Animator anim { get; protected set; }
    public SpriteRenderer sr { get; protected set; }

    // RIGIDBODY
    public Rigidbody2D rb { get; protected set; }

    // MOVEMENT
    public IMoveInput moveInput;
    public ILookInput lookInput;
    public IMoveable movement;
    public ICombat combat;

    public Queue<Vector2> forceQueue;

    // PARTICLE SYSTEMS
    public ParticleSystem dustPS;

    protected virtual void Awake()
    {
        // COMPONENTS
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // FORCE QUEUE
        forceQueue = new Queue<Vector2>();

        // GET MOVE INPUT COMPONENT
        moveInput = GetComponent<IMoveInput>();
        lookInput = GetComponent<ILookInput>();
        movement = GetComponent<IMoveable>();
        combat = GetComponent<ICombat>();
    }

    // START
    protected virtual void Start()
    {
        
    }

    // UPDATE
    protected virtual void Update()
    {
        
    }

    // FIXED UPDATE
    protected virtual void FixedUpdate()
    {
        if (forceQueue.Count > 0) ApplyForces();
    }

    // MOVE DUST
    public void MoveDust()
    {
        // DUST PS
        dustPS.transform.position = transform.position - ((Vector3)rb.velocity * 0.2f);
        if (rb.velocity.magnitude > 2.5f){
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
