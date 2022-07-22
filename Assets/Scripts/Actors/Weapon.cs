using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // SPRITES & ANIMATIONS
    protected Animator anim;
    protected SpriteRenderer sr;

    // WEAPON PARAMETERS
    protected WeaponParams wpnParams;
    protected int burstCount;

    // TIMER ARRAY
    protected float[] wpnTimers;

    // DIFFERENT WEAPON TIMERS ENUM
    protected enum WeaponTimers {
        fireTimer = 0,
        burstTimer = 1,
        end = 2
    }

    // AWAKE
    void Awake()
    {
        wpnTimers = new float[(int)WeaponTimers.end];
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        wpnParams = new WeaponParams(0.15f, 0.15f, 1f, 15f, 0.5f, 0.15f, 0);
    }

    // FIXED UPDATE
    void FixedUpdate()
    {
        UpdateTimers();
    }

    // UPDATE WEAPON ANIMATION STATE
    public void UpdateWeapon(Vector3 facingDir)
    {
        if (anim != null && sr != null)
        {
            anim.SetFloat("velX", facingDir.x);
            anim.SetFloat("velY", facingDir.y);
            if (facingDir.y > Mathf.Abs(facingDir.x)) {
                sr.sortingOrder = -1;
            } else {
                sr.sortingOrder = 1;
            }
        }
    }

    // FIRE WEAPON
    public void Fire(Vector3 origin, Vector3 target)
    { 
        if (!(wpnTimers[(int)WeaponTimers.fireTimer] > 0)) {
            MuzzleFlash(origin, 1f);
            wpnTimers[(int)WeaponTimers.fireTimer] = wpnParams.frate;
        }       
    }

    // DISPLAY MUZZLE FLASH
    protected void MuzzleFlash(Vector3 origin, float size)
    {
        GameManager.boomPS.Emit(origin,Vector3.zero, size + Random.Range(0.75f,1.25f), 0.0625f, Color.white);
    }

    // UPDATE FIRE TIMER
    private void UpdateTimers()
    {
        for (int i = 0; i < wpnTimers.Length; i++){
            if (wpnTimers[i] > 0f) wpnTimers[i] -= Time.deltaTime;
        }
    }
}
