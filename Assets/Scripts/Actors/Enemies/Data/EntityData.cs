using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "newPlayerData", menuName="Data/Player Data/Base Data")]
public class EntityData //: ScriptableObject
{
    public float moveSpd = 3f;
    public float animSpd = 0.35f;
    public Vector2 facingDir;
}
