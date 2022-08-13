using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //public static InputManager input;
    public static InputManager input;
    public InputSystem instance;
    
    public InputAction move;
    public InputAction look;

    public InputAction R1;
    public InputAction R2;
    public InputAction L1;
    public InputAction L2;

    public InputAction B;
    public InputAction Y;
    public InputAction A;
    public InputAction X;
    
    public InputAction up;
    public InputAction down;
    public InputAction left;
    public InputAction right;
    
    public InputAction start;

    // ON ENABLE
    private void OnEnable()
    {
        // STICKS
        move = instance.Player.LeftStick;
        move.Enable();

        look = instance.Player.RightStick;
        look.Enable();

        // SHOULDERS
        R1 = instance.Player.R1;
        R1.Enable();
        R2 = instance.Player.R2;
        R2.Enable();
        L1 = instance.Player.L1;
        L1.Enable();
        L2 = instance.Player.L2;
        L2.Enable();

        // BUTTONS
        B = instance.Player.B;
        B.Enable();
        Y = instance.Player.Y;
        Y.Enable();
        A = instance.Player.A;
        A.Enable();
        X = instance.Player.X;
        X.Enable();

        // D-PAD
        up = instance.Player.Up;
        up.Enable();
        down = instance.Player.Down;
        down.Enable();
        left = instance.Player.Left;
        left.Enable();
        right = instance.Player.Right;
        right.Enable();
        
        start = instance.Player.Start;
        start.Enable();
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

        start.Disable();
    }

    void Awake()
    {
        instance = new InputSystem();
        input = this;
    }
}
