using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public static SpawnPointManager instance;
    public List<GameObject> spawnPoints;
    public EnemyManager enemyManager;

    public float difficulty = 1;
    public int waves = 10;
    public int waveSize = 10;
    public float waveCount = 0;

    void Awake()
    {
        instance = this;
        spawnPoints = new List<GameObject>();
        enemyManager = EnemyManager.instance;
    }

    void Start()
    {
        spawnPoints = SpawnPointGenerator.spawnPoints;
    }

    void Update()
    {
        
    }

    public void UpdateWaves()
    {
        waveSize += Mathf.RoundToInt(difficulty + waveCount);
    }
}

public enum EnemyTypes
{
    shell,
    germinite,
    gland
}