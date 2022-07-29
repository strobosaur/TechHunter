using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookInput : MonoBehaviour, ILookInput
{
    Vector2 ILookInput.GetLookInput()
    {
        return Vector2.zero;
    }
}
