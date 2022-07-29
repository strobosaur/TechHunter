using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRanged : Weapon
{
    public override void WeaponAttack(Vector3 origin, Vector3 target)
    {
        // CHECK IF FIRE TIMER IS 0 & BURST COUNT IS UNDER LIMIT
        if ((burstCount <= wpnParams.burst) && !(wpnTimers[(int)WeaponTimers.burstTimer] > 0)) {
            // SET FIRE RATE TIMER
            if (burstCount <= 0) wpnTimers[(int)WeaponTimers.fireTimer] = wpnParams.frate;

            // INCREMENT BURST
            burstCount++;

            // FIRE ACTUAL WEAPON
            MuzzleFlash(origin, 1f);

            var targetDist = Vector3.Distance(origin, target);
            var ob = WeaponManager.instance.SpawnBullet();
            var rnd = Random.insideUnitCircle * (Mathf.Min(wpnParams.range, targetDist) * wpnParams.spr * 0.25f);

            target.x += rnd.x;
            target.y += rnd.y;
            
            ob.transform.position = origin;
            ob.moveDelta = wpnParams.bulletSpd * Random.Range(0.9f, 1.9f);
            ob.target = target;
            ob.tag = owner.tag;
            ob.targetLayer = 3;
            ob.shooter = owner;
            ob.damage = new DoDamage{damage = wpnParams.dmg * Random.Range(1f - wpnParams.dmgSpr, 1f), force = 1f};
            
            // SET BURST TIMER
            wpnTimers[(int)WeaponTimers.burstTimer] = wpnParams.brate;
        }
    }
}
