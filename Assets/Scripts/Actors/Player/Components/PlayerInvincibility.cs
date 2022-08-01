using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvincibility : MonoBehaviour
{
    public Player player;
    public bool isInvincible;

    private float alphaMod;
    private float alphaRate = 0.1f;

    private void Awake()
    {
        player = GetComponent<Player>();
        isInvincible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAlpha();
    }

    // UPDATE INVINCIBILITY TIMER
    public void UpdateInvincibilityTimer()
    {
        if (isInvincible)
        {
            if (Time.time > player.stats2.lastDamage + player.stats2.invincibilityTime.GetValue())
            {
                DisableInvincible();
            }
        }
    }

    public void SetInvincible()
    {
        isInvincible = true;
        alphaMod = 1f;
    }

    public void DisableInvincible()
    {
        isInvincible = false;
        alphaMod = 1f;
        player.sr.color = Color.white;
    }

    private void UpdateAlpha()
    {
        if (isInvincible)
        {
            Color col = player.sr.color;

            if (alphaMod > 0)
            {
                alphaMod -= alphaRate;
            } else
            {
                alphaMod = 1f;
            }

            col.a = 0.25f + ((0.5f * alphaMod) * Random.Range(0.75f, 1.25f));

            player.sr.color = col;
        }
    }
}
