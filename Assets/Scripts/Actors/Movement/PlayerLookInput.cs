using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookInput : MonoBehaviour, ILookInput
{
    Vector2 inputVector;
    public Vector2 GetLookInput()
    {
        // LEFT STICK
        inputVector = InputManager.input.look.ReadValue<Vector2>();
        if (inputVector.magnitude < Globals.G_RS_DEADZONE){
            inputVector = Vector2.zero;
        }

        return inputVector;        
    }
}
