using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void UpdateWeapon(Vector3 moveDelta)
    {
        anim.SetFloat("velX", moveDelta.x);
        anim.SetFloat("velY", moveDelta.y);
    }
}
