using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomWalk
{
    public static HashSet<Vector2Int> RandomWalkGen(Vector2Int origin, int steps, float chance = 0.2f)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(origin);
        var prevPos = origin;

        for(int i = 0; i < steps; i++)
        {
            var newPos = prevPos + Direction2D.GetRandomDir();
            path.Add(newPos);
            prevPos = newPos;
        }

        return path;
    }

    
}

public static class Direction2D
{
    public static List<Vector2Int> dirList = new List<Vector2Int>
    {
        new Vector2Int(0,1),
        new Vector2Int(1,0),
        new Vector2Int(0,-1),
        new Vector2Int(-1,0)
    };

    public static Vector2Int GetRandomDir()
    {
        return dirList[Random.Range(0, dirList.Count)];
    }
}