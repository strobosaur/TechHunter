using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer sr;

    private Vector3 targetPos;
    private Vector3 moveDelta;
    private float playerDist;
    private float playerMinDist = 0.75f;
    private float playerMaxDist = 10f;
    private float moveSpd = 0.15f;
    public bool isVisible = true;

    // AWAKE
    void Awake()
    {
        player = GameObject.Find(Globals.G_PLAYERNAME).transform;
        sr = GetComponent<SpriteRenderer>();
    }

    // FIXED UPDATE
    void FixedUpdate()
    {
        // MOVE CROSSHAIR
        transform.position = moveDelta;
    }

    // UPDATE CROSSHAIR POSITION & ALPHA
    public void UpdateCrosshair(Vector2 axis)
    {
        if (isVisible)
        {
            // DISTANCE TO PLAYER
            playerDist = Vector2.Distance(transform.position, player.position);

            if (playerDist < playerMinDist) {
                sr.enabled = false;
            } else {
                sr.enabled = true;
            }

            // SET TARGET POSITION TO PLAYER POS + R STICK * MAX DISTANCE
            targetPos = new Vector2(player.position.x,player.position.y) + (axis * playerMaxDist);

            // SMOOTH MOVEMENT BY LERPING & FIX COORDINATES TO WHOLE PIXELS
            moveDelta = Vector2.Lerp(transform.position, targetPos, moveSpd);
            moveDelta = Globals.PPPos(moveDelta);

            // SET ALPHA BASED ON DISTANCE TO PLAYER
            float alpha = Globals.EaseOutSine(Mathf.Max(0f, playerDist - playerMinDist) / (playerMaxDist - playerMinDist), 0f, 1f, 1f);
            Color col = sr.color;
            col.a = alpha;
            sr.color = col;
        } else if (sr.enabled == true) {
            sr.enabled = false;
        }
    }

    public void ToggleVisibility(bool show = true)
    {
        isVisible = show;
        if (show)
        {
            sr.enabled = true;
        } else {
            sr.enabled = false;
        }
    }
}
