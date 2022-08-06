using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Weapon")]
public class WeaponParams : Equipment
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
    public StatInt shots;     // HOW MANY SHOTS ARE FIRED AT ONCE
    public StatInt burst;  // NUMBER OF SHOTS IN ONE BURST
}
