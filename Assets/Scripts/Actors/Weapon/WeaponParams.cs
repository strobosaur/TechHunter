using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WeaponParams
{
    public bool isMelee;
    public Stat frate;     // TIME BETWEEN FIRE
    public Stat brate;     // TIME BETWEEN BURST SHOTS
    public Stat dmg;       // BASE DAMAGE
    public Stat range;     // BASE RANGE
    public Stat spr;       // SPREAD OF SHOT DIRECTION
    public Stat dmgSpr;    // HOW RANDOM THE DAMAGE IS
    public Stat bulletSpd; // BULLET SPEED
    public Stat knockback; // BULLET SPEED
    public StatInt pierce;    // HOW MANY TARGETS CAN BE PIERCED
    public StatInt shots;     // HOW MANY SHOTS ARE FIRED AT ONCE
    public StatInt burst;  // NUMBER OF SHOTS IN ONE BURST

    public WeaponParams(bool isMelee, float fireRate = 0, float burstRate = 0, float damage = 0, float range = 0, float spread = 0, float damageSpread = 0, float bulletSpd = 0, float knockback = 0, int pierce = 0, int shots = 1, int burst = 0)
    {
        this.isMelee = isMelee;
        
        this.frate = new Stat();
        this.frate.SetValue(fireRate);
        
        this.brate = new Stat();
        this.brate.SetValue(burstRate);
        
        this.dmg = new Stat();
        this.dmg.SetValue(damage);
        
        this.range = new Stat();
        this.range.SetValue(range);
        
        this.spr = new Stat();
        this.spr.SetValue(spread);
        
        this.dmgSpr = new Stat();
        this.dmgSpr.SetValue(damageSpread);
        
        this.bulletSpd = new Stat();
        this.bulletSpd.SetValue(bulletSpd);
        
        this.knockback = new Stat();
        this.knockback.SetValue(knockback);
        
        this.pierce = new StatInt();
        this.pierce.SetValue(pierce);
        
        this.shots = new StatInt();
        this.shots.SetValue(shots);
        
        this.burst = new StatInt();
        this.burst.SetValue(burst);
    }    
}
