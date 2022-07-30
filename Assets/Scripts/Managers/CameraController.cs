using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Player player;
    private Transform playerTransform;
    private Transform crossTransform;
    private Vector2 targetPos;
    private Vector2 moveDelta;
    private float camDistMove = 1f;
    private float moveSpd = 0.075f;

    // AWAKE
    void Awake()
    {
        player = GameObject.Find(Globals.G_PLAYERNAME).GetComponent<Player>();
        playerTransform = GameObject.Find(Globals.G_PLAYERNAME).transform;
        crossTransform = GameObject.Find("Crosshair").transform;
    }

    // UPDATE
    void Update()
    {
        if (player == null) player = GameObject.Find(Globals.G_PLAYERNAME).GetComponent<Player>();
        if (playerTransform == null) playerTransform = GameObject.Find(Globals.G_PLAYERNAME).transform;
        if (crossTransform == null) crossTransform = GameObject.Find("Crosshair").transform;
    }

    // FIXED UPDATE
    void FixedUpdate()
    {
        UpdateCameraPosition();
    }

    // LATE UPDATE
    void LateUpdate()
    {
        transform.position = new Vector3(moveDelta.x, moveDelta.y, transform.position.z);
    }

    // UPDATE CAMERA POSITION
    public void UpdateCameraPosition()
    {
        // IF PLAYER AIMING
        if (InputManager.input.look.ReadValue<Vector2>().magnitude > 0)
        {
            targetPos = Vector2.Lerp(playerTransform.position, crossTransform.position, 0.33f);
        } else if (InputManager.input.move.ReadValue<Vector2>().magnitude > 0) {
            targetPos = playerTransform.position + ((Vector3)player.rb.velocity * camDistMove);
        } else {
            targetPos = playerTransform.position;
        }

        // SMOOTH MOVEMENT BY LERPING & FIX COORDINATES TO WHOLE PIXELS
        moveDelta = Vector2.Lerp(transform.position, targetPos, moveSpd);
        moveDelta = Globals.PPPos(moveDelta);
    }

    // ACTUALLY MOVE CAMERA
    public void MoveCamera()
    {

    }
}
