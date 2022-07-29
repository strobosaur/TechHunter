using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour, ICombat
{
    public Player player;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    public void Attack(Weapon weapon, Vector2 origin, Vector2 target)
    {
        // COMBAT
        if ((weapon != null) && (InputManager.input.R2.IsPressed()))
        {
            // AIM
            player.FireWeapon(player.crosshair.transform.position);
        }
    }
}
