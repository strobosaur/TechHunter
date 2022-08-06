using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsObject
{
    public StatInt HPmax = new StatInt();
    public float HPcur;

    public Stat armor = new Stat();

    public Stat staminaMax = new Stat();
    public float staminaCur;

    public Stat moveSpd = new Stat();

    public Stat moveBoostSpd = new Stat();
    public Stat moveBoostTime = new Stat();
    public Stat moveBoostCD = new Stat();
    
    public Stat invincibilityTime = new Stat();

    public WeaponStatsObject wpnStats = new WeaponStatsObject();

    public void TakeDamage(float damage)
    {
        HPcur -= damage / (1f + (armor.GetValue() * 0.2f));
    } 
}
