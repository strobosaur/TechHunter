using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public static SpawnPointManager instance;

    public List<GameObject> spawnPoints = new List<GameObject>();
    public EnemyManager enemyManager;

    public float difficulty;
    public float waveCount;
    float spawnMod = 0.75f;

    public float startTime;
    public float lastSpawnTime;
    public float nextSpawnTime;
    public float nextSpawnTimeBase = 45f;
    public bool isSpawning;

    void Awake()
    {
        instance = this;
        enemyManager = EnemyManager.instance;
        nextSpawnTimeBase = 45f;
        waveCount = 0;
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        // SUBSCRIBE TO EVENTS
        PlayerManager.instance.onGameOver -= EnemyManager.instance.spawnPointGenerator.DeleteAllSpawnPoints;
    }

    void Start()
    {
        PlayerManager.instance.onGameOver += EnemyManager.instance.spawnPointGenerator.DeleteAllSpawnPoints;
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

    // SPAWN NEXT WAVE
    public List<Enemy> SpawnNextWave(GameObject spawnPoint = null, int count = 1)
    {
        List<Enemy> waveList = new List<Enemy>();
        GameObject tempPoint;
        Vector2 spawnPos;

        for (int n = 0; n < count; n++)
        {
            if ((spawnPoint != null) && (spawnPoints.Count > 1))
            {
                do {
                    tempPoint = SpawnPointGenerator.spawnPoints[Random.Range(0, SpawnPointGenerator.spawnPoints.Count)];
                } while (tempPoint == spawnPoint);

                spawnPos = tempPoint.transform.position;

            } else {
                spawnPos = SpawnPointGenerator.spawnPoints[Random.Range(0, SpawnPointGenerator.spawnPoints.Count)].transform.position;
            }

            Vector2 spawnPosMod;

            float difficultyMod1 = (difficulty * 0.15f) + (waveCount * 0.1f);
            float difficultyMod2 = (difficulty * 0.4f) + (waveCount * 0.33f);
            int difficultyMod3;

            int shells = Mathf.RoundToInt(Random.Range(2,5) + Mathf.RoundToInt(Random.Range(difficultyMod1 * 1.25f, difficultyMod2 * 1.33f)) * spawnMod);
            int germinites = Mathf.RoundToInt(Random.Range(1,4) + Mathf.RoundToInt(Random.Range(difficultyMod1, difficultyMod2)) * spawnMod);
            int glands = Mathf.RoundToInt(Random.Range(0,3) + Mathf.RoundToInt(Random.Range(difficultyMod1, difficultyMod2)) * spawnMod);

            // SHELLS
            for (int k = 0; k < shells; k++)
            {
                difficultyMod3 = Mathf.RoundToInt(difficulty * Random.Range(0.1f, 0.5f));

                spawnPosMod = Random.insideUnitCircle.normalized * Random.Range(0, 1.5f);

                var ob = EnemyManager.instance.GetEnemyPool(0);
                ob.GetComponent<Enemy>().UpgradeStats(difficultyMod3);
                EnemyManager.instance.enemyList.Add(ob.GetComponent<Enemy>());
                waveList.Add(ob.GetComponent<Enemy>());

                ob.GetComponent<EnemyMoveInput>().target = GameObject.Find("Player").transform;
                ob.transform.position = spawnPos + spawnPosMod;
            }

            // GERMINITES
            for (int j = 0; j < germinites; j++)
            {
                difficultyMod3 = Mathf.RoundToInt(difficulty * Random.Range(0.1f, 0.5f));

                spawnPosMod = Random.insideUnitCircle.normalized * Random.Range(0, 1.5f);

                var ob = EnemyManager.instance.GetEnemyPool(1);
                ob.GetComponent<Enemy>().UpgradeStats(difficultyMod3);
                EnemyManager.instance.enemyList.Add(ob.GetComponent<Enemy>());
                waveList.Add(ob.GetComponent<Enemy>());

                ob.GetComponent<EnemyMoveInput>().target = GameObject.Find("Player").transform;
                ob.transform.position = spawnPos + spawnPosMod;
            }

            // GLANDS
            for (int i = 0; i < glands; i++)
            {
                difficultyMod3 = Mathf.RoundToInt(difficulty * Random.Range(0.1f, 0.5f));

                spawnPosMod = Random.insideUnitCircle.normalized * Random.Range(0, 1.5f);

                var ob = EnemyManager.instance.GetEnemyPool(2);
                ob.GetComponent<Enemy>().UpgradeStats(difficultyMod3);
                EnemyManager.instance.enemyList.Add(ob.GetComponent<Enemy>());
                waveList.Add(ob.GetComponent<Enemy>());

                ob.GetComponent<EnemyMoveInput>().target = GameObject.Find("Player").transform;
                ob.transform.position = spawnPos + spawnPosMod;
            }
        }

        waveCount++;
        return waveList;
    }
}

public enum EnemyTypes
{
    shell,
    germinite,
    gland
}