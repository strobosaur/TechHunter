using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentLevelManager : MonoBehaviour
{
    public static CurrentLevelManager instance;

    public float levelStartTime;
    public float levelDuration;
    public float levelTimeLeft = Mathf.Infinity;

    public bool levelStarted;
    public bool levelWon;

    public int levelKills;
    public int levelScraps;
    public int levelTechUnits;

    public System.Action onLevelStart;
    public System.Action onLevelWon;

    void OnEnable()
    {
        instance = this;
        levelStarted = false;
        levelWon = false;

        onLevelWon += DisableCounter;
    }

    void OnDisable()
    {
        onLevelWon -= DisableCounter;
    }

    void Update()
    {
        if ((levelStarted) && (levelTimeLeft > 0))
        {
            levelTimeLeft -= Time.deltaTime;
            HUDlevel.instance.UpdateTimer(levelTimeLeft);

        } else if ((levelStarted) && (!levelWon) && !(levelTimeLeft > 0)) {
            levelTimeLeft = 0;
            HUDlevel.instance.UpdateTimer(levelTimeLeft);

            LevelWin();
        }
    }

    public void StartLevel(float duration)
    {
        levelKills = 0;
        levelScraps = 0;
        levelTechUnits = 0;

        levelStarted = true;
        levelWon = false;
        levelDuration = duration;
        levelTimeLeft = duration;
        levelStartTime = Time.time;

        onLevelStart?.Invoke();
    }

    public void LevelWin()
    {
        int techUnits = 1 + Mathf.RoundToInt(((Random.Range(0.15f,1f))) * (LevelManager.instance.difficulty * 0.33f));
        levelTechUnits += techUnits;
        Inventory.instance.ChangeTechUnits(techUnits);
        Inventory.instance.MissionWin();
        levelWon = true;
        onLevelWon?.Invoke();
    }

    public void DisableCounter()
    {
        levelStarted = false;
    }
}
