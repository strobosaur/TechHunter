using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Stat moveBoost;
    public Stat moveBoostTime;
    public Stat moveBoostCD;

    public Stat invincibilityTime;
    public float lastDamage;

    public PlayerStats(float moveBoost, float moveBoostTime, float moveBoostCD, float invincibilityTime)
    {
        this.moveBoost = new Stat();
        this.moveBoost.SetValue(moveBoost);

        this.moveBoostTime = new Stat();
        this.moveBoostTime.SetValue(moveBoostTime);

        this.moveBoostCD = new Stat();
        this.moveBoostCD.SetValue(moveBoostCD);

        this.invincibilityTime = new Stat();
        this.invincibilityTime.SetValue(invincibilityTime);
    }
}
