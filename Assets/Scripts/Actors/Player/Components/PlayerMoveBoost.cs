using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveBoost : MonoBehaviour
{
    public Player player;
    public float lastBoost;
    private bool isBoosting;

    // AWAKE
    void Awake()
    {
        player = GetComponent<Player>();
        isBoosting = false;
        lastBoost = Time.time;
    }

    // UPDATE
    void Update()
    {
        if ((Time.time > lastBoost + player.stats.moveBoostTime.GetValue()) && (isBoosting)) {
            player.stats.moveSpd.RemoveModifier(player.stats.moveBoostSpd.GetValue());
            isBoosting = false;
        }
    }

    // BOOST MOVE SPEED
    public void MoveBoost()
    {
        if (Time.time > lastBoost + player.stats.moveBoostCD.GetValue()){
            player.stats.moveSpd.AddModifier(player.stats.moveBoostSpd.GetValue());
            lastBoost = Time.time;
            isBoosting = true;
        }
    }
}
