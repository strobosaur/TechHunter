// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class WeaponMelee : Weapon, IWeapon
// {
//     protected override void Awake()
//     {
//         base.Awake();
//         isMelee = true;
//     }

//     public override void WeaponAttack(Vector3 origin, Transform target, WeaponStatsObject wpnParams)
//     {
//         if (Vector2.Distance(origin, target.position) <= wpnParams.range.GetValue())
//         {
//             var burst = wpnParams.burst.GetValue();
//             var shots = wpnParams.shots.GetValue();
            
//             // CHECK IF FIRE TIMER IS 0 & BURST COUNT IS UNDER LIMIT
//             if ((burstCount <= burst) && !(wpnTimers[(int)WeaponTimers.burstTimer] > 0)) {
//                 // SET FIRE RATE TIMER
//                 if (burstCount <= 0) wpnTimers[(int)WeaponTimers.fireTimer] = wpnParams.frate.GetValue();

//                 // INCREMENT BURST
//                 burstCount++;

//                 // FIRE ACTUAL WEAPON
//                 //MuzzleFlash(origin, 1f);

//                 // MAKE CORRECT AMOUNT OF SHOTS
//                 for (int i = 0; i < shots; i++)
//                 {
//                     // CHECK FOR HIT
//                     var targetOb = target.GetComponent<IDamageable>();
//                     if (targetOb != null)
//                     {
//                         var damage = new DoDamage{damage = wpnParams.dmg.GetValue() * Random.Range(1f - wpnParams.dmgSpr.GetValue(), 1f), force = wpnParams.knockback.GetValue()};
//                         targetOb.ReceiveDamage(damage, (target.position - origin).normalized);
//                     }
//                 }

//                 // var collision = (Physics2D.CircleCast(origin, 4f, (target.position - origin).normalized));
//                 // {
//                 //     Debug.Log("Melee hit " + collision.collider);
//                 //     Debug.Log("Owner Tag: " + owner.tag + " | Collider Tag: " + collision.collider.tag);
//                 //     if (owner.tag != collision.collider.tag)
//                 //     {
//                 //         var damage = new DoDamage{damage = wpnParams.dmg.GetValue() * Random.Range(1f - wpnParams.dmgSpr.GetValue(), 1f), force = wpnParams.knockback.GetValue()};
//                 //         if (collision.collider.GetComponent<IDamageable>() != null)
//                 //             collision.collider.GetComponent<IDamageable>().ReceiveDamage(damage, (collision.transform.position - transform.position).normalized);
//                 //     }        
//                 // }
                
//                 // SET BURST TIMER
//                 wpnTimers[(int)WeaponTimers.burstTimer] = wpnParams.brate.GetValue();
//             }
//         }
//     }
// }
