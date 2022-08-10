using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity Stats", menuName = "Stats/Stats")]
public class EntityStats : ScriptableObject
{
    public string entityName;

    public StatInt HPmax;

    public Stat armor;

    public Stat staminaMax;

    public Stat moveSpd;

    public Stat moveBoostSpd;
    public Stat moveBoostTime;
    public Stat moveBoostCD;
    
    public Stat invincibilityTime;
}
