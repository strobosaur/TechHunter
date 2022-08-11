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

    void OnEnable()
    {
        EnsureMinDistance();
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
}
