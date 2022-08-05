using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // STATIC INSTANCE
    public static GameManager instance;

    // MANAGER STATE
    public MenuManager menu;

    // PERSISTENT MANAGERS
    public PlayerManager playerManager;
    public DisplayManager displayManager;
    public MenuManager menuManager;
    public CameraController cam;

    // MANAGER STATE MACHINE
    public StateMachine stateMachine;
    public StateManagerMenu stateMenu;
    public StateManagerMenuExit stateMenuExit;
    public StateManagerBase stateBase;
    public StateManagerLevel stateLevel;

    // CAMERA BLACKSCREEN
    public Blackscreen blackscreen;

    // AWAKE
    void Awake()
    {
        // CREATE SINGLETON
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        // GET MANAGER COMPONENTS
        playerManager = GetComponent<PlayerManager>();
        displayManager = GetComponent<DisplayManager>();
        cam = Camera.main.GetComponent<CameraController>();
        blackscreen = cam.blackscreen;

        // INIT STATE MACHINE
        stateMachine = new StateMachine();
        stateMenu = new StateManagerMenu(this, stateMachine);
        stateMenuExit = new StateManagerMenuExit(this, stateMachine);
        stateBase = new StateManagerBase(this, stateMachine);
        stateLevel = new StateManagerLevel(this, stateMachine);

        // DISABLE MOUSE CURSOR
        Cursor.visible = false;
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
    }
}

public enum MainMenuOptions {
    newGame,
    highscores,
    quit
}