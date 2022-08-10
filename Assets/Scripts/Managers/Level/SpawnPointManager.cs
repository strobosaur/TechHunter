using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public static SpawnPointManager instance;

    public List<GameObject> spawnPoints = new List<GameObject>();
    public EnemyManager enemyManager;

    public float difficulty;
    public int waves = 10;
    public int waveSize = 10;
    public float waveCount = 0;

    public float startTime;
    public float lastSpawnTime;
    public float nextSpawnTime;
    public float nextSpawnTimeBase = 45f;
    public bool isSpawning;

    void Awake()
    {
        instance = this;
        enemyManager = EnemyManager.instance;
        nextSpawnTimeBase = 35f;
    }

    void OnEnable()
    {
        // SUBSCRIBE TO EVENTS
        //CurrentLevelManager.instance.onLevelStart += StartSpawning;
        CurrentLevelManager.instance.onLevelWon += StopSpawning;
        PlayerManager.instance.onGameOver += StopSpawning;
        PlayerManager.instance.onGameOver += EnemyManager.instance.spawnPointGenerator.DeleteAllSpawnPoints;
    }

    void OnDisable()
    {
        // SUBSCRIBE TO EVENTS
        //CurrentLevelManager.instance.onLevelStart -= StartSpawning;
        CurrentLevelManager.instance.onLevelWon -= StopSpawning;
        PlayerManager.instance.onGameOver -= StopSpawning;
        PlayerManager.instance.onGameOver -= EnemyManager.instance.spawnPointGenerator.DeleteAllSpawnPoints;
    }

    void Start()
    {
        spawnPoints = SpawnPointGenerator.spawnPoints;
    }

    void Update()
    {
        if (SpawnPointGenerator.spawnPoints.Count > 0)
        {
            if (isSpawning) {
                if (nextSpawnTime > 0) {
                    nextSpawnTime -= Time.deltaTime;
                } else {
                    SpawnNextWave();
                    nextSpawnTime = SetNextSpawnTime();
                }
            }
        }
    }

    public float SetNextSpawnTime()
    {        
        return ((nextSpawnTimeBase * Random.Range(0.75f, 1.25f)) / (1f + (difficulty * Random.Range(0f, 0.2f))));
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    public void StartSpawning()
    {
        isSpawning = true;
        difficulty = LevelManager.instance.difficulty;
        waveCount = 0;
        startTime = Time.time;

        nextSpawnTime = SetNextSpawnTime();
        SpawnNextWave();
    }

    public void SpawnNextWave()
    {
        Vector2 spawnPos = SpawnPointGenerator.spawnPoints[Random.Range(0, SpawnPointGenerator.spawnPoints.Count)].transform.position;
        Vector2 spawnPosMod;

        float difficultyMod1 = (difficulty * 0.15f) + (waveCount * 0.1f);
        float difficultyMod2 = (difficulty * 0.4f) + (waveCount * 0.33f);
        int difficultyMod3;

        int shells = Random.Range(2,5) + Mathf.RoundToInt(Random.Range(difficultyMod1 * 1.25f, difficultyMod2 * 1.33f));
        int germinites = Random.Range(1,4) + Mathf.RoundToInt(Random.Range(difficultyMod1, difficultyMod2));
        int glands = Random.Range(0,3) + Mathf.RoundToInt(Random.Range(difficultyMod1, difficultyMod2));

        // SHELLS
        for (int k = 0; k < shells; k++)
        {
            difficultyMod3 = Mathf.RoundToInt(difficulty * Random.Range(0.1f, 0.5f));

            spawnPosMod = Random.insideUnitCircle * 2.5f;

            var ob = EnemyManager.instance.GetEnemyPool(0);
            ob.GetComponent<Enemy>().UpgradeStats(difficultyMod3);
            EnemyManager.instance.enemyList.Add(ob.GetComponent<Enemy>());

            ob.GetComponent<EnemyMoveInput>().target = GameObject.Find("Player").transform;
            ob.transform.position = spawnPos + spawnPosMod;
        }

        // GERMINITES
        for (int j = 0; j < germinites; j++)
        {
            difficultyMod3 = Mathf.RoundToInt(difficulty * Random.Range(0.1f, 0.5f));

            spawnPosMod = Random.insideUnitCircle * 2.5f;

            var ob = EnemyManager.instance.GetEnemyPool(1);
            ob.GetComponent<Enemy>().UpgradeStats(difficultyMod3);
            EnemyManager.instance.enemyList.Add(ob.GetComponent<Enemy>());

            ob.GetComponent<EnemyMoveInput>().target = GameObject.Find("Player").transform;
            ob.transform.position = spawnPos + spawnPosMod;
        }

        // GLANDS
        for (int i = 0; i < glands; i++)
        {
            difficultyMod3 = Mathf.RoundToInt(difficulty * Random.Range(0.1f, 0.5f));

            spawnPosMod = Random.insideUnitCircle * 2.5f;

            var ob = EnemyManager.instance.GetEnemyPool(2);
            ob.GetComponent<Enemy>().UpgradeStats(difficultyMod3);
            EnemyManager.instance.enemyList.Add(ob.GetComponent<Enemy>());

            ob.GetComponent<EnemyMoveInput>().target = GameObject.Find("Player").transform;
            ob.transform.position = spawnPos + spawnPosMod;
        }

        waveCount++;
    }
}

public enum EnemyTypes
{
    shell,
    germinite,
    gland
}