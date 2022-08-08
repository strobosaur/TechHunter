using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMissionItem
{
    public BaseMissionItem(string name, int difficulty, int size)
    {
        this.name = name;
        this.difficulty = difficulty;
        this.size = size;
    }
    
    public string name;
    public int difficulty;
    public int size;
}
