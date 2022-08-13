using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // SINGLETON
    public static LevelManager instance;

    // SCENE NAMES
    public string[] sceneNames = {"01_MainMenu", "02_Base", "03_Level"};

    public System.Action onMenuSceneLoaded;
    public System.Action onBaseSceneLoaded;
    public System.Action onLevelSceneLoaded;

    public float difficulty { get; private set; }
    public BaseMissionItem nextLevel { get; private set; }
    public bool levelGenerated = false;

    private string sceneName;

    // AWAKE
    void Awake()
    {
        instance = this;
        difficulty = 1;
    }

    // RESET GAME SESSION
    public void ResetGameSession()
    {
        difficulty = 0;
        levelGenerated = false;
        nextLevel = null;
    }

    // SET NEXT LEVEL PARAMETERS
    public void SetNextLevel(BaseMissionItem level)
    {
        nextLevel = level;
        levelGenerated = false;
    }

    // ADD DIFFICULTY
    public void AddDifficulty(float amount)
    {
        difficulty = Mathf.Max(0f, difficulty + amount);
    }

    // LOAD SCENE FUNCTION
    public void LoadScene(string sceneName) 
    {
        // LOAD SCENE
        this.sceneName = sceneName;
        SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += InitState;
    }

    public void InitState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= InitState;
        // FIRE SUBSCRIBED EVENTS
        if (s.name == sceneNames[(int)SceneName.MainMenu]) {

            // ENTER MAIN MENU SCENE
            onMenuSceneLoaded?.Invoke();
            GameManager.instance.stateMachine.ChangeState(GameManager.instance.stateMenu);

        } else if (s.name == sceneNames[(int)SceneName.InBase]) {

            // ENTER BASE SCENE
            onBaseSceneLoaded?.Invoke();
            GameManager.instance.stateMachine.ChangeState(GameManager.instance.stateBase);

        } else if (s.name == sceneNames[(int)SceneName.InLevel]) {

            // ENTER LEVEL SCENE
            onLevelSceneLoaded?.Invoke();
            GameManager.instance.stateMachine.ChangeState(GameManager.instance.stateLevel);
        }
    }
}

public enum SceneName {
    MainMenu, InBase, InLevel
}