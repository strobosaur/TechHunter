using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 LS;
    
    void Update()
    {
        // LEFT STICK
        LS = InputManager.input.move.ReadValue<Vector2>().normalized;
        if (LS.magnitude < Globals.G_LS_DEADZONE){
            LS = Vector3.zero;
        }

        GetComponent<IMoveVelocity>().SetVelocity(LS);
    }
}
