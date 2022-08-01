using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // SPRITES & ANIMATIONS
    protected Animator anim;
    protected SpriteRenderer sr;
    protected Fighter owner;

    // WEAPON PARAMETERS
    public WeaponParams wpnParams { get; protected set; }
    protected int burstCount;

    public bool isMelee;

    // TIMER ARRAY
    protected float[] wpnTimers;

    // DIFFERENT WEAPON TIMERS ENUM
    protected enum WeaponTimers {
        fireTimer = 0,
        burstTimer = 1,
        end = 2
    }

    // AWAKE
    protected virtual void Awake()
    {
        wpnTimers = new float[(int)WeaponTimers.end];
        owner = GetComponent<Fighter>();
    }

    // FIXED UPDATE
    protected virtual void FixedUpdate()
    {
        UpdateTimers();
    }

    // DISPLAY MUZZLE FLASH
    protected void MuzzleFlash(Vector3 origin, float size)
    {
        EffectsManager.boomPS.Emit(origin,Vector3.zero, size * Random.Range(0.9f,1.1f), 0.0625f, Color.white);
    }

    // UPDATE FIRE TIMER
    private void UpdateTimers()
    {
        // LOOP THROUGH TIMER LIST
        for (int i = 0; i < wpnTimers.Length; i++){
            if (wpnTimers[i] > 0f) wpnTimers[i] -= Time.deltaTime;
        } 
        
        // CHECK FOR FIRE RATE TIMER & RESET BURSTS
        if ((burstCount > 0) && !(wpnTimers[(int)WeaponTimers.fireTimer] > 0)) {
            // SET TIMERS
            burstCount = 0;
        }
    }

    public void SetWeaponParams(WeaponParams stats)
    {
        wpnParams = stats;
    }

    public WeaponParams GetWeaponParams()
    {
        return wpnParams;
    }

    public virtual void WeaponAttack(Vector3 origin, Transform target){}
}
