using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public float lastSpawn;
    public float spawnCDBase = 60f;
    public float spawnCD = 60f;
    public float spawnMultiplier = 0.25f;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnCD *= Random.Range(0.75f, 1.25f);
        SpawnEnemies();
        lastSpawn = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastSpawn + spawnCD)
        {
            SpawnEnemies();
            lastSpawn = Time.time;
            spawnCD = spawnCDBase * Random.Range(0.75f, 1.25f);
        }
    }

    public void SpawnEnemies()
    {
        int glandCount = Mathf.RoundToInt(Random.Range(0,4) + Mathf.RoundToInt(Random.Range(0f, 0.75f)) + Mathf.RoundToInt(Random.Range(0f, 0.625f)) * spawnMultiplier);
        int germCount = Mathf.RoundToInt(Random.Range(3,8) + Mathf.RoundToInt(Random.Range(0f, 0.75f)) + Mathf.RoundToInt(Random.Range(0f, 0.625f)) * spawnMultiplier);
        int shellCount = Mathf.RoundToInt(Random.Range(3,12) + Mathf.RoundToInt(Random.Range(0f, 0.75f)) + Mathf.RoundToInt(Random.Range(0f, 0.625f)) * spawnMultiplier);

        // GLANDS
        for (int i = 0; i < glandCount; i++)
        {
            var pos = Random.insideUnitCircle * 2f;
            var ob = Instantiate(EnemyManager.instance.enemyPrefabList[2]);
            EnemyManager.instance.enemyList.Add(ob.GetComponent<Enemy>());

            ob.GetComponent<EnemyMoveInput>().target = GameObject.Find("Player").transform;
            ob.transform.position = pos + (Vector2)transform.position;
        }

        // GERMINITES
        for (int j = 0; j < germCount; j++)
        {
            var pos = Random.insideUnitCircle * 2f;
            var ob = Instantiate(EnemyManager.instance.enemyPrefabList[1]);
            EnemyManager.instance.enemyList.Add(ob.GetComponent<Enemy>());

            ob.GetComponent<EnemyMoveInput>().target = GameObject.Find("Player").transform;
            ob.transform.position = pos + (Vector2)transform.position;
        }

        // SHELLS
        for (int k = 0; k < shellCount; k++)
        {
            var pos = Random.insideUnitCircle * 2f;
            var ob = Instantiate(EnemyManager.instance.enemyPrefabList[0]);
            EnemyManager.instance.enemyList.Add(ob.GetComponent<Enemy>());

            ob.GetComponent<EnemyMoveInput>().target = GameObject.Find("Player").transform;
            ob.transform.position = pos + (Vector2)transform.position;
        }
    }
}
