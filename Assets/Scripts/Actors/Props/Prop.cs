using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public string propName;

    public void FlipSprite()
    {
        if (Random.value < 0.5f)
            transform.localScale = new Vector3(-1,1,1);
    }
}
