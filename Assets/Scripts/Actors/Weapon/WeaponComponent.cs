using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponent : MonoBehaviour, IWeapon
{
    // SPRITES & ANIMATIONS
    protected Animator anim;
    public SpriteRenderer sr { get; protected set; }
    protected Fighter owner;

    // WEAPON PARAMETERS
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

    // FIRE WEAPON
    public void WeaponAttack(Vector3 origin, Transform target, WeaponStatsObject wpnParams)
    {
        if (wpnParams.isMelee)
        {
            WeaponAttackMelee(origin, target, wpnParams);
        } else {
            WeaponAttackRanged(origin, target, wpnParams);
        }
    }

    // FIRE WEAPON RANGED
    public void WeaponAttackRanged(Vector3 origin, Transform target, WeaponStatsObject wpnParams)
    {
        var burst = wpnParams.burst.GetValue();
        var shots = wpnParams.shots.GetValue();
        Vector2 targetDir = (target.position - origin).normalized;

        // CHECK IF FIRE TIMER IS 0 & BURST COUNT IS UNDER LIMIT
        if ((burstCount < burst) && !(wpnTimers[(int)WeaponTimers.burstTimer] > 0)) {
            // SET FIRE RATE TIMER
            if (burstCount <= 0) wpnTimers[(int)WeaponTimers.fireTimer] = wpnParams.frate.GetValue();

            // INCREMENT BURST
            burstCount++;

            // FIRE ACTUAL WEAPON
            MuzzleFlash(origin, 1f);

            // PLAY SOUND EFFECT
            if (owner.tag == "Player") AudioManager.instance.Play("shoot");
            else AudioManager.instance.Play("melee");

            // FIRE SET AMOUNT OF TIMES
            for (int i = 0; i < shots; i++)
            {
                //var targetDist = Vector3.Distance(origin, target.position);
                var targetPos = (Vector2)origin + (targetDir * wpnParams.range.GetValue());
                Bullet ob;
                if (owner.tag == "Player") ob = WeaponManager.instance.SpawnBullet();
                else ob = WeaponManager.instance.SpawnBullet(true);
                //var rnd = Random.insideUnitCircle * (Mathf.Min(wpnParams.range.GetValue(), targetDist) * wpnParams.spr.GetValue() * 0.25f);
                var rnd = Random.insideUnitCircle * wpnParams.range.GetValue() * wpnParams.spr.GetValue() * 0.25f;

                targetPos.x += rnd.x;
                targetPos.y += rnd.y;
                
                ob.transform.position = origin;
                ob.moveDelta = wpnParams.bulletSpd.GetValue() * Random.Range(0.9f, 1.9f);
                ob.target = targetPos;
                ob.tag = owner.tag;
                ob.targetLayer = 3;
                ob.shooter = owner;
                ob.damage = new DoDamage{damage = wpnParams.dmg.GetValue() * Random.Range(1f - wpnParams.dmgSpr.GetValue(), 1f), force = wpnParams.knockback.GetValue()};
            }
            
            // SET BURST TIMER
            wpnTimers[(int)WeaponTimers.burstTimer] = wpnParams.brate.GetValue();
        }
    }

    // WEAPON ATTACK MELEE
    private void WeaponAttackMelee(Vector3 origin, Transform target, WeaponStatsObject wpnParams)
    {
        if (Vector2.Distance(origin, target.position) <= wpnParams.range.GetValue())
        {
            var burst = wpnParams.burst.GetValue();
            var shots = wpnParams.shots.GetValue();
            
            // CHECK IF FIRE TIMER IS 0 & BURST COUNT IS UNDER LIMIT
            if ((burstCount <= burst) && !(wpnTimers[(int)WeaponTimers.burstTimer] > 0)) {
                // SET FIRE RATE TIMER
                if (burstCount <= 0) wpnTimers[(int)WeaponTimers.fireTimer] = wpnParams.frate.GetValue();

                // INCREMENT BURST
                burstCount++;

                // FIRE ACTUAL WEAPON
                //MuzzleFlash(origin, 1f);

                // PLAY SOUND EFFECT
                AudioManager.instance.Play("melee");

                // MAKE CORRECT AMOUNT OF SHOTS
                for (int i = 0; i < shots; i++)
                {
                    // CHECK FOR HIT
                    var targetOb = target.GetComponent<IDamageable>();
                    if (targetOb != null)
                    {
                        var damage = new DoDamage{damage = wpnParams.dmg.GetValue() * Random.Range(1f - wpnParams.dmgSpr.GetValue(), 1f), force = wpnParams.knockback.GetValue()};
                        targetOb.ReceiveDamage(damage, (target.position - origin).normalized);
                    }
                }
                
                // SET BURST TIMER
                wpnTimers[(int)WeaponTimers.burstTimer] = wpnParams.brate.GetValue();
            }
        }
    }
}
