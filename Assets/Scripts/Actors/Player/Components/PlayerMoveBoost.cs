using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveBoost : MonoBehaviour
{
    public Player player;
    public float lastBoost;
    private bool isBoosting;

    void Awake()
    {
        player = GetComponent<Player>();
        isBoosting = false;
    }

    void Update()
    {
        if ((Time.time > lastBoost + player.stats2.moveBoostTime.GetValue()) && (isBoosting)) {
            player.stats.moveSpd.RemoveModifier(player.stats2.moveBoost.GetValue());
            isBoosting = false;
        }
    }

    public void MoveBoost()
    {
        if (Time.time > lastBoost + player.stats2.moveBoostCD.GetValue()){
            player.stats.moveSpd.AddModifier(player.stats2.moveBoost.GetValue());
            lastBoost = Time.time;
            isBoosting = true;
        }
    }
}
