using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomWalk
{
    public static HashSet<Vector2Int> RandomWalkGen(Vector2Int origin, int steps, float chance = 0.5f)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(origin);
        var prevPos = origin;
        var dir = Direction2D.GetRandomDir();

        for(int i = 0; i < steps; i++)
        {            
            var newPos = prevPos + dir;
            path.Add(newPos);
            prevPos = newPos;

            dir = Random.Range(0f,1f) < chance ? Direction2D.GetRandomDir() : dir;
        }

        return path;
    }
    
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int origin, int steps)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomDir();
        var currPos = origin;
        corridor.Add(currPos);

        for (int i = 0; i < steps; i++)
        {
            currPos += direction;
            corridor.Add(currPos);
        }

        return corridor;
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