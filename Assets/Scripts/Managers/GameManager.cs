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

    public StateManagerStartGame stateStartGame;
    public StateManagerMenu stateMenu;
    public StateManagerMenuEnter stateMenuEnter;
    public StateManagerMenuHighscores stateMenuHighscores;
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
            Globals.DestroyAllChildren(gameObject);
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
        stateStartGame = new StateManagerStartGame(this, stateMachine);
        stateMenuEnter = new StateManagerMenuEnter(this, stateMachine);
        stateMenu = new StateManagerMenu(this, stateMachine);
        stateMenuHighscores = new StateManagerMenuHighscores(this, stateMachine);
        stateMenuExit = new StateManagerMenuExit(this, stateMachine);
        stateBase = new StateManagerBase(this, stateMachine);
        stateLevel = new StateManagerLevel(this, stateMachine);
        stateLevelWon = new StateManagerLevelWon(this, stateMachine);
        stateLevelOver = new StateManagerLevelOver(this, stateMachine);

        // DISABLE MOUSE CURSOR
        Cursor.visible = false;

        // GET CURRENT SCENE
        currentScene = SceneManager.GetActiveScene();
    }

    // START
    void Start()
    {
        stateMachine.Initialize(stateStartGame);
    }

    // UPDATE
    void Update()
    {
        // ESCAPE & GAME QUIT
        if (InputManager.input.start.WasPressedThisFrame()) { Application.Quit(); }
        
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