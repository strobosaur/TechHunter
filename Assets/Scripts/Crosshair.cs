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
    private float moveSpd = 0.1f;

    // AWAKE
    void Awake()
    {
        player = GameObject.Find(Globals.G_PLAYERNAME).transform;
        sr = GetComponent<SpriteRenderer>();
    }

    // UPDATE
    void Update()
    {

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
        // DISTANCE TO PLAYER
        playerDist = Vector2.Distance(transform.position, player.position);

        // SET TARGET POSITION TO PLAYER POS + R STICK * MAX DISTANCE
        targetPos = new Vector2(player.position.x,player.position.y) + (axis * playerMaxDist);

        // SMOOTH MOVEMENT BY LERPING & FIX COORDINATES TO WHOLE PIXELS
        moveDelta = Vector2.Lerp(transform.position, targetPos, moveSpd);
        moveDelta = new Vector3(
            Mathf.RoundToInt(moveDelta.x * Globals.G_CELLSIZE) / Globals.G_CELLSIZE, 
            Mathf.RoundToInt(moveDelta.y * Globals.G_CELLSIZE) / Globals.G_CELLSIZE, 0f);

        // SET ALPHA BASED ON DISTANCE TO PLAYER
        float alpha = Globals.EaseOutSine(Mathf.Max(0f, playerDist - playerMinDist) / (playerMaxDist - playerMinDist), 0f, 1f, 1f);
        Color col = sr.color;
        col.a = alpha;
        sr.color = col;
    }
}
