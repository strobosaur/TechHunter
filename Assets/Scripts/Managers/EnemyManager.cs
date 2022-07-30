using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyManager : MonoBehaviour
{
    public AstarPath astar;
    public static EnemyManager instance;

    public SpawnPointManager spawnPointManager;

    public Dictionary<string, EntityStats> enemyStats = new Dictionary<string, EntityStats>();
    public List<GameObject> enemyPrefabList = new List<GameObject>();
    public List<Enemy> enemyList = new List<Enemy>();

    void Awake()
    {
        instance = this;
        astar = GameObject.Find("A*").GetComponent<AstarPath>();
        spawnPointManager = GetComponent<SpawnPointManager>();
    }
}
