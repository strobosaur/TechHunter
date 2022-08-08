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

    // AWAKE
    void Awake()
    {
        instance = this;
        difficulty = 0;
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
        //var scene = SceneManager.LoadSceneAsync(sceneName);
        SceneManager.LoadScene(sceneName);
        //scene.allowSceneActivation = false;

        // do {

        // } while (scene.progress < 0.9f);

        // ALLOW SCENE ACTIVATION
        //scene.allowSceneActivation = true;

        // FIRE SUBSCRIBED EVENTS
        if (sceneName == sceneNames[(int)SceneName.MainMenu]) {

            // ENTER MAIN MENU SCENE
            onMenuSceneLoaded?.Invoke();
            GameManager.instance.stateMachine.ChangeState(GameManager.instance.stateMenu);

        } else if (sceneName == sceneNames[(int)SceneName.InBase]) {

            // ENTER BASE SCENE
            onBaseSceneLoaded?.Invoke();
            GameManager.instance.stateMachine.ChangeState(GameManager.instance.stateBase);

        } else if (sceneName == sceneNames[(int)SceneName.InLevel]) {

            // ENTER LEVEL SCENE
            onLevelSceneLoaded?.Invoke();
            GameManager.instance.stateMachine.ChangeState(GameManager.instance.stateLevel);
        }
    }
}

public enum SceneName {
    MainMenu, InBase, InLevel
}