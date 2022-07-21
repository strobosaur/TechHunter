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
    private float maxDist = 10f;
    private float moveSpd = 0.1f;

    void Awake()
    {
        player = GameObject.Find(Globals.G_PLAYERNAME).transform;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDist = Vector2.Distance(transform.position, player.position);
    }

    void FixedUpdate()
    {
        transform.position = moveDelta;
    }

    public void UpdateCrosshair(Vector2 axis)
    {
        targetPos = new Vector2(player.position.x,player.position.y) + (axis * maxDist);
        // targetPos = new Vector3(
        //     Mathf.RoundToInt(targetPos.x * Globals.G_CELLSIZE) / Globals.G_CELLSIZE, 
        //     Mathf.RoundToInt(targetPos.y * Globals.G_CELLSIZE) / Globals.G_CELLSIZE);

        moveDelta = Vector2.Lerp(transform.position, targetPos, moveSpd);
        moveDelta = new Vector3(
            Mathf.RoundToInt(moveDelta.x * Globals.G_CELLSIZE) / Globals.G_CELLSIZE, 
            Mathf.RoundToInt(moveDelta.y * Globals.G_CELLSIZE) / Globals.G_CELLSIZE, 0f);
    }
}
