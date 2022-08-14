using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour, ICombat
{
    public Player player;
    public Animator wpnAnim;
    public SpriteRenderer wpnSr;

    void Awake()
    {
        player = GetComponent<Player>();
        if (transform.GetChild(2) != null) wpnAnim = transform.GetChild(2).GetComponent<Animator>();
        if (transform.GetChild(2) != null) wpnSr = transform.GetChild(2).GetComponent<SpriteRenderer>();
    }

    public void Attack(IWeapon weapon, Vector2 origin, Transform target, WeaponStatsObject wpnStats)
    {
        if (GameManager.instance.stateMachine.CurrentState == GameManager.instance.stateLevel)
        {
            // COMBAT
            if ((weapon != null) && (InputManager.input.R2.IsPressed()))
            {
                // AIM
                player.FireWeapon(weapon, player.crosshair.transform, wpnStats);
            }
        }
    }

    // UPDATE WEAPON ANIMATION STATE
    public void UpdateWeapon(Vector2 facingDir)
    {
        if (wpnAnim != null && wpnSr != null)
        {
            wpnAnim.SetFloat("velX", facingDir.x);
            wpnAnim.SetFloat("velY", facingDir.y);
            if (facingDir.y > Mathf.Abs(facingDir.x)) {
                wpnSr.sortingOrder = -1;
            } else {
                wpnSr.sortingOrder = 1;
            }
        }
    }
}
