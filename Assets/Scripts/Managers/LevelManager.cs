using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float difficulty { get; private set; }
    public BaseMissionItem nextLevel { get; private set; }

    void Awake()
    {
        difficulty = 0;
    }

    public void SetNextLevel(BaseMissionItem level)
    {
        nextLevel = level;
    }

    public void AddDifficulty(float amount)
    {
        difficulty = Mathf.Max(0f, difficulty + amount);
    }
}
