using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveInput : MonoBehaviour, IMoveInput
{
    Vector2 inputVector;
    public Vector2 GetMoveInput()
    {
        // LEFT STICK
        inputVector = InputManager.input.move.ReadValue<Vector2>();
        if (inputVector.magnitude < Globals.G_LS_DEADZONE){
            inputVector = Vector2.zero;
        }

        return inputVector;
    }
}
