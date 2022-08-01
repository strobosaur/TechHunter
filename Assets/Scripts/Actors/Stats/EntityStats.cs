using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public int HPmax = 10;
    public float HPcur = 10;
    public Stat armor;
    public Stat staminaMax;
    public Stat staminaCur;
    public Stat moveSpd;
    public Stat moveBoost;
    public Stat moveBoostTime;
    public Stat moveBoostCD;

    public void TakeDamage(float damage)
    {
        HPcur -= damage / (1f + (armor.GetValue() * 0.1f));
    }
}
