using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMelee : Weapon, IWeapon
{
    public override void WeaponAttack(Vector3 origin, Vector3 target)
    {
        if (Vector2.Distance(origin, target) <= wpnParams.range)
        {
            // CHECK IF FIRE TIMER IS 0 & BURST COUNT IS UNDER LIMIT
            if ((burstCount <= wpnParams.burst) && !(wpnTimers[(int)WeaponTimers.burstTimer] > 0)) {
                // SET FIRE RATE TIMER
                if (burstCount <= 0) wpnTimers[(int)WeaponTimers.fireTimer] = wpnParams.frate;

                // INCREMENT BURST
                burstCount++;

                // FIRE ACTUAL WEAPON
                //MuzzleFlash(origin, 1f);                

                // CHECK FOR HIT
                var collision = (Physics2D.CircleCast(origin, 4f, (target - origin).normalized));                
                {
                    Debug.Log("Melee hit " + collision);
                    if (owner.tag != collision.collider.tag)
                    {
                        var damage = new DoDamage{damage = wpnParams.dmg * Random.Range(1f - wpnParams.dmgSpr, 1f), force = 1f};
                        if (collision.collider.GetComponent<IDamageable>() != null)
                            collision.collider.GetComponent<IDamageable>().ReceiveDamage(damage, (collision.transform.position - transform.position).normalized);
                    }        
                }
                
                // SET BURST TIMER
                wpnTimers[(int)WeaponTimers.burstTimer] = wpnParams.brate;
            }
        }
    }
}
