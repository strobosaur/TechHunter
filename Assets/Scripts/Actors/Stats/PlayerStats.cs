using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Stat moveBoost;
    public Stat moveBoostTime;
    public Stat moveBoostCD;

    public PlayerStats(float moveBoost, float moveBoostTime, float moveBoostCD)
    {
        this.moveBoost = new Stat();
        this.moveBoost.SetValue(moveBoost);

        this.moveBoostTime = new Stat();
        this.moveBoostTime.SetValue(moveBoostTime);

        this.moveBoostCD = new Stat();
        this.moveBoostCD.SetValue(moveBoostCD);
    }
}
