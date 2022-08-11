using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveBoost : MonoBehaviour
{
    public Player player;
    public float lastBoost;
    private float regenCD = 1.5f;
    private float regenRate = 0.02f;
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
        if ((isBoosting) && (player.stats.staminaCur > 0)) {
            player.stats.staminaCur -= regenRate * 2f;
            lastBoost = Time.time;
        } else {
            isBoosting = false;
            RemoveModifier();
        }
    }

    // FIXED UPDATE
    void FixedUpdate()
    {
        if ((player.stats.staminaCur < player.stats.staminaMax.GetValue()) && (Time.time > lastBoost + regenCD))
            RegenStamina();
    }

    // BOOST MOVE SPEED
    public void MoveBoost()
    {
        if (player.stats.staminaCur > 0)
        {
            if (!isBoosting) {
                isBoosting = true;
                player.stats.moveSpd.AddModifier(player.stats.moveBoostSpd.GetValue());
                lastBoost = Time.time;
            }
        }
    }

    public void RemoveModifier()
    {
        player.stats.moveSpd.RemoveModifier(player.stats.moveBoostSpd.GetValue());
        isBoosting = false;
    }

    private void RegenStamina()
    {
        player.stats.staminaCur = Globals.Approach(player.stats.staminaCur, player.stats.staminaMax.GetValue(), regenRate);
    }
}
