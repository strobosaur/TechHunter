using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsObject : MonoBehaviour
{
    public StatInt HPmax;
    public float HPcur;

    public Stat armor;

    public Stat staminaMax;
    public float staminaCur;

    public Stat moveSpd;

    public Stat moveBoostSpd;
    public Stat moveBoostTime;
    public Stat moveBoostCD;
    
    public Stat invincibilityTime;

    public WeaponStatsObject weaponStats;

    public void TakeDamage(float damage)
    {
        HPcur -= damage / (1f + (armor.GetValue() * 0.2f));
    } 
}
