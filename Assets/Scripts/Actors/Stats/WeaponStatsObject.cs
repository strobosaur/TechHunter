using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStatsObject
{
    public bool isMelee;
    public Stat frate = new Stat();     // TIME BETWEEN FIRE
    public Stat brate = new Stat();     // TIME BETWEEN BURST SHOTS
    public Stat dmg = new Stat();       // BASE DAMAGE
    public Stat range = new Stat();     // BASE RANGE
    public Stat spr = new Stat();       // SPREAD OF SHOT DIRECTION
    public Stat dmgSpr = new Stat();    // HOW RANDOM THE DAMAGE IS
    public Stat bulletSpd = new Stat(); // BULLET SPEED
    public Stat knockback = new Stat(); // BULLET SPEED
    public StatInt shots = new StatInt();     // HOW MANY SHOTS ARE FIRED AT ONCE
    public StatInt burst = new StatInt();  // NUMBER OF SHOTS IN ONE BURST

    public void InitStats(WeaponParams stats)
    {
        isMelee = stats.isMelee;
        frate.SetValue(stats.frate.GetValue());
        brate.SetValue(stats.brate.GetValue());
        dmg.SetValue(stats.dmg.GetValue());
        range.SetValue(stats.range.GetValue());
        spr.SetValue(stats.spr.GetValue());
        dmgSpr.SetValue(stats.dmgSpr.GetValue());
        bulletSpd.SetValue(stats.bulletSpd.GetValue());
        knockback.SetValue(stats.knockback.GetValue());
        shots.SetValue(stats.shots.GetValue());
        burst.SetValue(stats.burst.GetValue());
    }
}
