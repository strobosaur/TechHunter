using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Animator anim;
    private float fireRate = 0.1f;
    private float fireLast = 0;

    // AWAKE
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // FIXED UPDATE
    void FixedUpdate()
    {
        if (fireLast > 0)
            fireLast -= Time.deltaTime;
    }

    // UPDATE WEAPON ANIMATION STATE
    public void UpdateWeapon(Vector3 moveDelta)
    {
        anim.SetFloat("velX", moveDelta.x);
        anim.SetFloat("velY", moveDelta.y);
    }

    // UPDATE FIRE TRIGGER

    // FIRE WEAPON
    public void Fire(Vector3 origin)
    { 
        if (fireLast <= 0) {
            GameManager.boomPS.Emit(origin,Vector3.zero, Random.Range(0.75f,1.25f), 0.0625f, Color.white);
            fireLast = fireRate;
        }       
    }
}
