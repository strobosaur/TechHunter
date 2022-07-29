using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "newPlayerData", menuName="Data/Player Data/Base Data")]
public class PlayerData //: ScriptableObject
{
    public float moveSpd = 3f;
    public float animSpd = 1f;
    public Vector2 facingDir;
}
