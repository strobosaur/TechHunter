using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // STATIC INSTANCE
    public static GameManager instance;

    // MANAGER STATE
    public MenuManager menu;
    public Scene currentScene;

    // PERSISTENT MANAGERS
    public PlayerManager playerManager;
    public DisplayManager displayManager;
    public MenuManager menuManager;
    public CameraController cam;
    public UpgradeManager upgManager;
    public LevelManager levelManager;

    // MANAGER STATE MACHINE
    public StateMachine stateMachine;
    public StateManagerMenu stateMenu;
    public StateManagerMenuExit stateMenuExit;
    public StateManagerBase stateBase;
    public StateManagerLevel stateLevel;
    public StateManagerLevelWon stateLevelWon;
    public StateManagerLevelOver stateLevelOver;

    // CAMERA BLACKSCREEN
    public Blackscreen blackscreen;

    // PLAYER
    public Player player;

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
        upgManager = GetComponent<UpgradeManager>();
        levelManager = GetComponent<LevelManager>();
        blackscreen = Camera.main.GetComponentInChildren<Blackscreen>();

        // INIT STATE MACHINE
        stateMachine = new StateMachine();
        stateMenu = new StateManagerMenu(this, stateMachine);
        stateMenuExit = new StateManagerMenuExit(this, stateMachine);
        stateBase = new StateManagerBase(this, stateMachine);
        stateLevel = new StateManagerLevel(this, stateMachine);
        stateLevelWon = new StateManagerLevelWon(this, stateMachine);
        stateLevelOver = new StateManagerLevelOver(this, stateMachine);

        // DISABLE MOUSE CURSOR
        //Cursor.visible = false;

        // GET CURRENT SCENE
        currentScene = SceneManager.GetActiveScene();
    }

    // START
    void Start()
    {
        switch (currentScene.name)
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