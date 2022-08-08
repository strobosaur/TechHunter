using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float difficulty { get; private set; }
    public BaseMissionItem nextLevel { get; private set; }
    public bool levelGenerated = false;

    void Awake()
    {
        difficulty = 0;
    }

    public void SetNextLevel(BaseMissionItem level)
    {
        nextLevel = level;
        levelGenerated = false;
    }

    public void AddDifficulty(float amount)
    {
        difficulty = Mathf.Max(0f, difficulty + amount);
    }
}
