using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentLevelManager : MonoBehaviour
{
    public static CurrentLevelManager instance;
    public static List<Enemy> currentWave = new List<Enemy>();

    public float levelStartTime;

    public bool levelStarted;
    public bool levelWon;
    public bool waveStarted = false;

    public int levelKills;
    public int levelScraps;
    public int levelTechUnits;

    public int pylonsPoweredOn = 0;
    private int pylonsToPowerOn;

    public System.Action onLevelStart;
    public System.Action onLevelWon;
    public System.Action onWaveCleared;
    public System.Action onPylonCountReached;

    void OnEnable()
    {
        instance = this;
        levelStarted = false;
        levelWon = false;
        waveStarted = false;
    }

    void OnDisable()
    {

    }

    void Update()
    {
        if (waveStarted)
        {
            if (currentWave.Count < 1)
            {
                onWaveCleared?.Invoke();
                waveStarted = false;
                pylonsPoweredOn++;
                Inventory.instance.totalPylonsPoweredOn++;
                
                HUDlevel.instance.UpdatePylons(pylonsPoweredOn, pylonsToPowerOn);

                if (pylonsPoweredOn >= pylonsToPowerOn) {
                    onPylonCountReached?.Invoke();
                }
            }
        }
    }

    public void StartLevel()
    {
        currentWave.Clear();

        levelKills = 0;
        levelScraps = 0;
        levelTechUnits = 0;
        pylonsPoweredOn = 0;

        int totalPylons = SpawnPointGenerator.pylonList.Count;
        int pylonDifficultyMod = Mathf.Min(totalPylons, Mathf.RoundToInt(SpawnPointGenerator.pylonList.Count * (LevelManager.instance.difficulty / 20f)));
        pylonsToPowerOn = Mathf.Max(1, pylonDifficultyMod);

        levelStarted = true;
        levelWon = false;
        waveStarted = false;

        levelStartTime = Time.time;

        HUDlevel.instance.UpdatePylons(pylonsPoweredOn, pylonsToPowerOn);

        onLevelStart?.Invoke();
    }

    // START WAVE
    public void StartWave(GameObject spawnPoint)
    {
        waveStarted = true;
        currentWave = SpawnPointManager.instance.SpawnNextWave(spawnPoint, pylonsPoweredOn + 1);
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
}
