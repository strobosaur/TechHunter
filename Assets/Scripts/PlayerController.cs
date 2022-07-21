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

    // INPUT SYSTEM
    public InputSystem inputSystem;
    
    private InputAction move;
    private InputAction look;

    private InputAction R1;
    private InputAction R2;
    private InputAction L1;
    private InputAction L2;

    private InputAction B;
    private InputAction Y;
    private InputAction A;
    private InputAction X;
    
    private InputAction up;
    private InputAction down;
    private InputAction left;
    private InputAction right;
    
    private Vector2 LS;

    // AWAKE
    void Awake()
    {
        inputSystem = new InputSystem();
        anim = GetComponent<Animator>();
        crosshair = GameObject.Find("Crosshair").GetComponent<Crosshair>();
    }

    // ON ENABLE
    private void OnEnable()
    {
        // STICKS
        move = inputSystem.Player.LeftStick;
        move.Enable();

        look = inputSystem.Player.RightStick;
        look.Enable();

        // SHOULDERS
        R1 = inputSystem.Player.R1;
        R1.Enable();
        R2 = inputSystem.Player.R2;
        R2.Enable();
        L1 = inputSystem.Player.L1;
        L1.Enable();
        L2 = inputSystem.Player.L2;
        L2.Enable();

        // BUTTONS
        B = inputSystem.Player.B;
        B.Enable();
        Y = inputSystem.Player.Y;
        Y.Enable();
        A = inputSystem.Player.A;
        A.Enable();
        X = inputSystem.Player.X;
        X.Enable();

        // D-PAD
        up = inputSystem.Player.Up;
        up.Enable();
        down = inputSystem.Player.Down;
        down.Enable();
        left = inputSystem.Player.Left;
        left.Enable();
        right = inputSystem.Player.Right;
        right.Enable();
    }

    // ON DISABLE
    private void OnDisable()
    {
        // STICKS
        move.Disable();
        look.Disable();

        // SHOULDERS
        R1.Disable();
        R2.Disable();
        L1.Disable();
        L2.Disable();
        
        // BUTTONS
        B.Disable();
        Y.Disable();
        A.Disable();
        X.Disable();
        
        // D-PAD
        up.Disable();
        down.Disable();
        left.Disable();
        right.Disable();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        moveSpd = 3f;        
    }

    // Update is called once per frame
    void Update()
    {
        // GET MOVE INPUT
        moveInput = move.ReadValue<Vector2>();

        // UPDATE CROSSHAIR
        crosshair.UpdateCrosshair(look.ReadValue<Vector2>());

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
}
