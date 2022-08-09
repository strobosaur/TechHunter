using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public float lastSpawn;
    public float spawnCDBase = 60f;
    public float spawnCD = 60f;
    public float spawnMultiplier = 0.1f;
    public bool isSpawning = true;
    public Queue<int> spawnQueue = new Queue<int>();
    
    // Start is called before the first frame update
    void Start()
    {
        // spawnCD *= Random.Range(0.75f, 1.25f);
        // SpawnEnemies();
        // lastSpawn = Time.time;
    }

    void OnEnable()
    {
        EnsureMinDistance();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Time.time > lastSpawn + spawnCD)
        // {
        //     SpawnEnemies();
        //     lastSpawn = Time.time;
        //     spawnCD = spawnCDBase * Random.Range(0.75f, 1.25f);
        // }
    }

    // ENSURE MIN DIST
    private void EnsureMinDistance()
    {
        foreach (var spawnPoint in SpawnPointGenerator.spawnPoints)
        {
            if (spawnPoint != this) {
                if (Vector2.Distance(transform.position, spawnPoint.transform.position) < 10f) {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void SpawnEnemies()
    {
        int shellCount = 0;
        int germiniteCount = 0;
        int glandCount = 0;
        int allCount = 0;

        int waveSize = SpawnPointManager.instance.waveSize;
        //int spawnPoints = SpawnPointManager.instance.spawnPoints.Count;
        int spawnPoints = SpawnPointGenerator.spawnPoints.Count;

        // CALCULATE WAVE SIZE
        while (allCount < Mathf.CeilToInt(waveSize / spawnPoints))
        {
            glandCount += Mathf.RoundToInt(Random.Range(0,2) + Mathf.RoundToInt(Random.Range(0f, 0.75f)) + Mathf.RoundToInt(Random.Range(0f, 0.625f)));
            germiniteCount += Mathf.RoundToInt(Random.Range(1,3) + Mathf.RoundToInt(Random.Range(0f, 0.75f)) + Mathf.RoundToInt(Random.Range(0f, 0.625f)));
            shellCount += Mathf.RoundToInt(Random.Range(1,4) + Mathf.RoundToInt(Random.Range(0f, 0.75f)) + Mathf.RoundToInt(Random.Range(0f, 0.625f)));

            allCount = shellCount + germiniteCount + glandCount;
        }

        // GLANDS
        for (int i = 0; i < glandCount; i++)
        {
            var pos = Random.insideUnitCircle * 2f;
            //var ob = Instantiate(EnemyManager.instance.enemyPrefabList[2]);
            var ob = EnemyManager.instance.GetEnemyPool(2);
            EnemyManager.instance.enemyList.Add(ob.GetComponent<Enemy>());

            ob.GetComponent<EnemyMoveInput>().target = GameObject.Find("Player").transform;
            ob.transform.position = pos + (Vector2)transform.position;
        }

        // GERMINITES
        for (int j = 0; j < germiniteCount; j++)
        {
            var pos = Random.insideUnitCircle * 2f;
            //var ob = Instantiate(EnemyManager.instance.enemyPrefabList[1]);
            var ob = EnemyManager.instance.GetEnemyPool(1);
            EnemyManager.instance.enemyList.Add(ob.GetComponent<Enemy>());

            ob.GetComponent<EnemyMoveInput>().target = GameObject.Find("Player").transform;
            ob.transform.position = pos + (Vector2)transform.position;
        }

        // SHELLS
        for (int k = 0; k < shellCount; k++)
        {
            var pos = Random.insideUnitCircle * 2f;
            //var ob = Instantiate(EnemyManager.instance.enemyPrefabList[0]);
            var ob = EnemyManager.instance.GetEnemyPool(0);
            EnemyManager.instance.enemyList.Add(ob.GetComponent<Enemy>());

            ob.GetComponent<EnemyMoveInput>().target = GameObject.Find("Player").transform;
            ob.transform.position = pos + (Vector2)transform.position;
        }

        // UPDATE WAVE COUNT
        SpawnPointManager.instance.waveCount += 1f / spawnPoints;
    }
}
