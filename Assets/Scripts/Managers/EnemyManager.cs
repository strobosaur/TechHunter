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

    void Awake()
    {
        instance = this;
        astar = GameObject.Find("A*").GetComponent<AstarPath>();
        spawnPointManager = GetComponent<SpawnPointManager>();
    }    
}
