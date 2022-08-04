using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // PLAYER VARIABLES
    public Player player;
    public Transform playerTransform;
    public Transform crossTransform;

    // TARGET & MOVEMENT PARAMETERS
    public Vector2 targetPos;
    public Vector2 moveDelta;
    public float camDistMove = 1f;
    public float moveSpd = 0.075f;

    // STATE MACHINE
    CameraStateMachine stateMachine;
    CameraStateMenu stateMenu;
    CameraStateBase stateBase;
    CameraStateLevel stateLevel;

    // AWAKE
    void Awake()
    {
        // CREATE STATE MACHINE
        stateMachine = new CameraStateMachine();
        stateMenu = new CameraStateMenu(this, stateMachine);
        stateBase = new CameraStateBase(this, stateMachine);
        stateLevel = new CameraStateLevel(this, stateMachine);
    }

    // START
    void Start()
    {
        stateMachine.Initialize(stateMenu);
    }

    // UPDATE
    void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }

    // FIXED UPDATE
    void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
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
        // SMOOTH MOVEMENT BY LERPING & FIX COORDINATES TO WHOLE PIXELS
        moveDelta = Vector2.Lerp(transform.position, targetPos, moveSpd);
        moveDelta = Globals.PPPos(moveDelta);
    }
}