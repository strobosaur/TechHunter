using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyManager : MonoBehaviour
{
    public AstarPath astar;
    public static EnemyManager instance;

    void Awake()
    {
        instance = this;
        astar = GameObject.Find("A*").GetComponent<AstarPath>();
    }
}
