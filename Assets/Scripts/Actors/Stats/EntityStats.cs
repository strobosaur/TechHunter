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

    public void TakeDamage(float damage)
    {
        HPcur -= damage / (1f + (armor.GetValue() * 0.1f));
    }
}
