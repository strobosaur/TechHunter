using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Movable
{
    // INPUT
    private Vector2 LS;
    private Vector2 RS;

    // ANIMATOR
    private Animator anim;
    private float animSpd = 1f;
    private Vector3 facingDir;
    private Vector2 firePivot;

    // PARTICLE SYSTEMS
    public ParticleSystem dustPS;

    // CROSSHAIR
    private Crosshair crosshair;

    // WEAPON
    private Weapon weapon;
    private float firePivotYmod = 0.75f;
    private float firePivotDist = 0.75f;

    // AWAKE
    void Awake()
    {
        anim = GetComponent<Animator>();
        crosshair = GameObject.Find("Crosshair").GetComponent<Crosshair>();
        weapon = GameObject.Find("Weapon").GetComponent<Weapon>();
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
        // GET STICK INPUT
        GetStickInput();

        // GET MOVE INPUT
        moveInput = LS;

        // GET AIM DIRECTION
        GetAimDir();

        // UPDATE CROSSHAIR
        crosshair.UpdateCrosshair(RS);

        // UPDATE ANIMATOR
        UpdateAnimator();

        // SPAWN MOVE DUST PARTICLES
        MoveDust();

        // FIRE WEAPON
        FireWeapon();
    }

    // FIXED UPDATE
    void FixedUpdate()
    {
        UpdateMotor(moveInput);
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

    // UPDATE ANIMATOR
    private void UpdateAnimator()
    {
        if (moveDelta.magnitude > 0.1f) {
            anim.SetFloat("velX", moveDelta.x);
            anim.SetFloat("velY", moveDelta.y);
            weapon.UpdateWeapon(moveDelta);

            // UPDATE ANIMATOR PARAMETERS
            anim.SetFloat("magnitude", moveDelta.magnitude);
            anim.speed = moveDelta.magnitude * animSpd;
        } else {
            // UPDATE ANIMATOR PARAMETERS
            anim.SetFloat("magnitude", 0f);
            anim.speed = 0f;
        }

        if (RS.magnitude > 0.2)
        {
            anim.SetFloat("velX", RS.x);
            anim.SetFloat("velY", RS.y);
            weapon.UpdateWeapon(RS);
        }
    }

    // GET AIM DIRECTION
    private void GetAimDir()
    {
        if (RS.magnitude > 0f)
        {
            firePivot = (crosshair.transform.position - transform.position).normalized;
            firePivot *= firePivotDist;
            firePivot.y *= firePivotYmod;
        }
    }

    private void FireWeapon()
    {
        if (InputManager.input.R2.IsPressed())
        {
            Vector2 muzzlePos = Random.insideUnitCircle * 0.15f;
            Vector3 firePos = new Vector3(transform.position.x + firePivot.x + muzzlePos.x, transform.position.y + firePivot.y + muzzlePos.y, 0f);

            weapon.Fire(firePos, crosshair.transform.position);
        }
    }
}
