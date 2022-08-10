using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraController : MonoBehaviour
{
    public CameraController instance;

    // PLAYER VARIABLES
    public Player player;
    public Transform playerTransform;
    public Transform crossTransform;

    // BLACKSCREEN & GUI
    public Blackscreen blackscreen;
    public TMP_Text interactableText;

    // TARGET & MOVEMENT PARAMETERS
    public Vector2 targetPos;
    public Vector2 moveDelta;
    public float camDistMove = 1f;
    public float moveSpd = 0.075f;

    // STATE MACHINE
    public CameraStateMachine stateMachine;
    public CameraStateMenu stateMenu;
    public CameraStateBase stateBase;
    public CameraStateLevel stateLevel;

    // AWAKE
    void Awake()
    {
        // CREATE SINGLETON
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Globals.DestroyAllChildren(gameObject);
            Destroy(gameObject);
        }

        // CREATE STATE MACHINE
        stateMachine = new CameraStateMachine();
        stateMenu = new CameraStateMenu(this, stateMachine);
        stateBase = new CameraStateBase(this, stateMachine);
        stateLevel = new CameraStateLevel(this, stateMachine);

        // BLACKSCREEN & GUI
        blackscreen = GetComponentInChildren<Blackscreen>();
        interactableText = GameObject.Find("InteractableMessage").GetComponent<TMP_Text>();
    }

    // START
    void Start()
    {
        switch (GameManager.instance.currentScene.name)
        {
            case "01_MainMenu":
            stateMachine.Initialize(stateMenu);
            break;

            case "02_Base":
            stateMachine.Initialize(stateBase);
            break;

            case "03_Level":
            stateMachine.Initialize(stateLevel);
            break;
        }
        //stateMachine.Initialize(stateMenu);
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