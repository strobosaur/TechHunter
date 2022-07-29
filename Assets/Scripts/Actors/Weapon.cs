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
    protected IWeapon weapon;
    protected WeaponParams wpnParams;
    protected int burstCount;
    protected float bulletSpd = 32f;

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
        wpnParams = new WeaponParams(1.5f, 0.1f, 1f, 15f, 0.5f, 0.15f, 8);
        owner = GetComponentInParent<Fighter>();
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
        // CHECK IF FIRE TIMER IS 0 & BURST COUNT IS UNDER LIMIT
        if ((burstCount <= wpnParams.burst) && !(wpnTimers[(int)WeaponTimers.burstTimer] > 0)) {
            // SET FIRE RATE TIMER
            if (burstCount <= 0) wpnTimers[(int)WeaponTimers.fireTimer] = wpnParams.frate;

            // INCREMENT BURST
            burstCount++;

            // FIRE ACTUAL WEAPON
            MuzzleFlash(origin, 1f);
            //weapon.FireWeapon(origin, target);

            var targetDist = Vector3.Distance(origin, target);
            var ob = WeaponManager.instance.SpawnBullet();
            var rnd = Random.insideUnitCircle * (targetDist * wpnParams.spr * 0.25f);

            target.x += rnd.x;
            target.y += rnd.y;
            
            ob.transform.position = origin;
            ob.moveDelta = bulletSpd * Random.Range(0.9f, 1.9f);
            ob.target = target;
            ob.tag = owner.tag;
            ob.targetLayer = 3;
            ob.shooter = owner;
            ob.damage = new DoDamage{damage = 1, force = 5f};
            
            // SET BURST TIMER
            wpnTimers[(int)WeaponTimers.burstTimer] = wpnParams.brate;
        }
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
}
