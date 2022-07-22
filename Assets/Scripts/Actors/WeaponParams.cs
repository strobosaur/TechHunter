using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WeaponParams
{
    public float frate;     // TIME BETWEEN FIRE
    public float brate;     // TIME BETWEEN BURST SHOTS
    public float dmg;       // BASE DAMAGE
    public float range;     // BASE RANGE
    public float spr;       // SPREAD OF SHOT DIRECTION
    public float dmgSpr;    // HOW RANDOM THE DAMAGE IS
    public int burst;       // NUMBER OF SHOTS IN ONE BURST

    public WeaponParams(float fireRate, float burstRate, float damage, float range, float spread, float damageSpread, int burst){
        this.frate = fireRate;
        this.brate = burstRate;
        this.dmg = damage;
        this.range = range;
        this.spr = spread;
        this.dmgSpr = damageSpread;
        this.burst = burst;
    }    
}
