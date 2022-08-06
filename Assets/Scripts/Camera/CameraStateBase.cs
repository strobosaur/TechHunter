using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateBase : CameraState
{
    // CONSTRUCTOR
    public CameraStateBase(CameraController camera, CameraStateMachine stateMachine) : base(camera, stateMachine){}

    // ENTER STATE
    public override void Enter()
    {
        base.Enter();
        
        camera.player = GameObject.Find(Globals.G_PLAYERNAME).GetComponent<Player>();
        camera.playerTransform = GameObject.Find(Globals.G_PLAYERNAME).transform;
        camera.crossTransform = GameObject.Find("Crosshair").transform;
    }

    // EXIT STATE
    public override void Exit()
    {
        base.Exit();
    }

    // LOGIC UPDATE
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (camera.player == null) camera.player = GameObject.Find(Globals.G_PLAYERNAME).GetComponent<Player>();
        if (camera.playerTransform == null) camera.playerTransform = GameObject.Find(Globals.G_PLAYERNAME).transform;
        if (camera.crossTransform == null) camera.crossTransform = GameObject.Find("Crosshair").transform;
        
        // IF PLAYER AIMING
        if (InputManager.input.look.ReadValue<Vector2>().magnitude > 0)
        {
            camera.targetPos = Vector2.Lerp(camera.playerTransform.position, camera.crossTransform.position, 0.33f);
        } else if (InputManager.input.move.ReadValue<Vector2>().magnitude > 0) {
            camera.targetPos = camera.playerTransform.position + ((Vector3)camera.player.rb.velocity * camera.camDistMove);
        } else {
            camera.targetPos = camera.playerTransform.position;
        }
    }

    // PHYSICS UPDATE
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
