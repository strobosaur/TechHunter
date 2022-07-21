using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Movable
{
    // ANIMATOR
    private Animator anim;
    private float animSpd = 1f;

    // PARTICLE SYSTEMS
    public ParticleSystem dustPS;

    // CROSSHAIR
    private Crosshair crosshair;

    // AWAKE
    void Awake()
    {
        anim = GetComponent<Animator>();
        crosshair = GameObject.Find("Crosshair").GetComponent<Crosshair>();
    }

    // START
    protected override void Start()
    {
        base.Start();
        moveSpd = 3f;        
    }

    // UPDATE
    void Update()
    {
        // GET MOVE INPUT
        moveInput = InputManager.input.move.ReadValue<Vector2>();

        // UPDATE CROSSHAIR
        crosshair.UpdateCrosshair(InputManager.input.look.ReadValue<Vector2>());

        // UPDATE ANIMATOR PARAMETERS
        anim.SetFloat("velX", moveDelta.x);
        anim.SetFloat("velY", moveDelta.y);
        anim.SetFloat("magnitude", moveDelta.magnitude);
        anim.speed = moveDelta.magnitude * animSpd;

        // DUST PS
        dustPS.transform.position = transform.position - (moveDelta * 0.2f);
        if (moveDelta.magnitude > 1f){
            if (!dustPS.isEmitting) {
                dustPS.Play();
            }
        } else dustPS.Stop();
    }

    // FIXED UPDATE
    void FixedUpdate()
    {
        UpdateMotor(moveInput);
    }

    // GET MOVE DELTA
    public Vector3 GetMoveDelta() { return moveDelta; }
}
