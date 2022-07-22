using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Fighter
{
    // INPUT
    private Vector2 LS;
    private Vector2 RS;

    // PARTICLE SYSTEMS
    public ParticleSystem dustPS;

    // CROSSHAIR
    private Crosshair crosshair;

    // AWAKE
    protected override void Awake()
    {
        base.Awake();
        crosshair = GameObject.Find("Crosshair").GetComponent<Crosshair>();
        weapon = GameObject.Find("Weapon").GetComponent<Weapon>();
    }

    // START
    protected void Start()
    {
        moveSpd = 3f;
        aimTarget = crosshair.transform.position;
    }

    // UPDATE
    protected override void Update()
    {
        base.Update();
        
        // GET STICK INPUT
        GetStickInput();

        // GET MOVE INPUT
        moveInput = LS;

        // UPDATE CROSSHAIR
        crosshair.UpdateCrosshair(RS);
        aimTarget = crosshair.transform.position;

        // SPAWN MOVE DUST PARTICLES
        MoveDust();

        // FIRE WEAPON
        if (InputManager.input.R2.IsPressed())
        {
            FireWeapon();
        }
    }

    // GET MOVE DELTA
    public Vector3 GetMoveDelta() { return moveDelta; }

    // GET STICK INPUT
    private void GetStickInput()
    {
        // LEFT STICK
        LS = InputManager.input.move.ReadValue<Vector2>();
        if (LS.magnitude < Globals.G_LS_DEADZONE){
            LS = Vector3.zero;
        }

        // RIGHT STICK
        RS = InputManager.input.look.ReadValue<Vector2>();
        if (RS.magnitude < Globals.G_RS_DEADZONE) {
            RS = Vector3.zero;
        }
    }

    // MOVE DUST
    private void MoveDust()
    {
        // DUST PS
        dustPS.transform.position = transform.position - (moveDelta * 0.2f);
        if (moveDelta.magnitude > 2f){
            if (!dustPS.isEmitting) {
                dustPS.Play();
            }
        } else dustPS.Stop();
    }
}
