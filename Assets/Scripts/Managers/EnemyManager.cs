using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    public AstarPath astar;
    public static EnemyManager instance;

    public SpawnPointManager spawnPointManager;

    public Dictionary<string, EntityStats> enemyStats = new Dictionary<string, EntityStats>();
    public List<GameObject> enemyPrefabList = new List<GameObject>();
    public List<Enemy> enemyList = new List<Enemy>();

    private ObjectPool<GameObject> shellPool;
    private ObjectPool<GameObject> germPool;
    private ObjectPool<GameObject> glandPool;

    void Awake()
    {
        instance = this;
        astar = GameObject.Find("A*").GetComponent<AstarPath>();
        spawnPointManager = GetComponent<SpawnPointManager>();

        // CREATE SHELL POOL
        shellPool = new ObjectPool<GameObject>(() => { 
            return Instantiate(enemyPrefabList[0]);
        }, shell => {
            shell.gameObject.SetActive(true);
        }, shell => {
            shell.gameObject.SetActive(false);
        }, shell => {
            Destroy(shell.gameObject);
        }, false, 100, 300);
        
        // CREATE GERMINITE POOL
        germPool = new ObjectPool<GameObject>(() => { 
            return Instantiate(enemyPrefabList[1]);
        }, germinite => {
            germinite.gameObject.SetActive(true);
        }, germinite => {
            germinite.gameObject.SetActive(false);
        }, germinite => {
            Destroy(germinite.gameObject);
        }, false, 100, 300);
        
        // CREATE GLAND POOL
        glandPool = new ObjectPool<GameObject>(() => { 
            return Instantiate(enemyPrefabList[2]);
        }, gland => {
            gland.gameObject.SetActive(true);
        }, gland => {
            gland.gameObject.SetActive(false);
        }, gland => {
            Destroy(gland.gameObject);
        }, false, 100, 300);
    }

    void Start()
    {

    }

    public GameObject GetEnemyPool(int index)
    {
        if (index == 0) return shellPool.Get();
        if (index == 1) return germPool.Get();
        if (index == 2) return glandPool.Get();
        else return shellPool.Get();
    }
}
