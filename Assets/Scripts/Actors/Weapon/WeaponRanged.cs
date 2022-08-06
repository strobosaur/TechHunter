// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class WeaponRanged : Weapon, IWeapon
// {
//     protected override void Awake()
//     {
//         base.Awake();
//         isMelee = false;
//     }

//     public override void WeaponAttack(Vector3 origin, Transform target, WeaponStatsObject wpnParams)
//     {
//         var burst = wpnParams.burst.GetValue();
//         var shots = wpnParams.shots.GetValue();

//         // CHECK IF FIRE TIMER IS 0 & BURST COUNT IS UNDER LIMIT
//         if ((burstCount <= burst) && !(wpnTimers[(int)WeaponTimers.burstTimer] > 0)) {
//             // SET FIRE RATE TIMER
//             if (burstCount <= 0) wpnTimers[(int)WeaponTimers.fireTimer] = wpnParams.frate.GetValue();

//             // INCREMENT BURST
//             burstCount++;

//             // FIRE ACTUAL WEAPON
//             MuzzleFlash(origin, 1f);

//             // FIRE SET AMOUNT OF TIMES
//             for (int i = 0; i < shots; i++)
//             {
//                 var targetDist = Vector3.Distance(origin, target.position);
//                 var targetPos = target.position;
//                 var ob = WeaponManager.instance.SpawnBullet();
//                 var rnd = Random.insideUnitCircle * (Mathf.Min(wpnParams.range.GetValue(), targetDist) * wpnParams.spr.GetValue() * 0.25f);

//                 targetPos.x += rnd.x;
//                 targetPos.y += rnd.y;
                
//                 ob.transform.position = origin;
//                 ob.moveDelta = wpnParams.bulletSpd.GetValue() * Random.Range(0.9f, 1.9f);
//                 ob.target = targetPos;
//                 ob.tag = owner.tag;
//                 ob.targetLayer = 3;
//                 //ob.pierce = wpnParams.pierce.GetValue();
//                 ob.shooter = owner;
//                 ob.damage = new DoDamage{damage = wpnParams.dmg.GetValue() * Random.Range(1f - wpnParams.dmgSpr.GetValue(), 1f), force = wpnParams.knockback.GetValue()};
//             }
            
//             // SET BURST TIMER
//             wpnTimers[(int)WeaponTimers.burstTimer] = wpnParams.brate.GetValue();
//         }
//     }
// }
