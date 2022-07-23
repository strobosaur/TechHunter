using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public TileManager tileManager;
    private Vector2Int startPos = Vector2Int.zero;
    public float chance = 0.5f;
    public int iterations = 25;
    public int walkLength = 100;
    public bool randomEachIteration = true;

    void Awake()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        HashSet<Vector2Int> floorPos = RunRandomWalk();
        floorPos = AddCardinalDirs(floorPos);
        tileManager.PaintFloorTiles(floorPos);
        WallFinder.MakeWalls(floorPos, tileManager);
    }

    public HashSet<Vector2Int> AddCardinalDirs(IEnumerable<Vector2Int> positions)
    {
        HashSet<Vector2Int> outSet = new HashSet<Vector2Int>();
        foreach (var pos in positions)
        {
            outSet.Add(pos);
            foreach (var dir in Direction2D.dirList)
            {
                outSet.Add(pos + dir);
            }    
        }

        return outSet;
    }

    private HashSet<Vector2Int> RunRandomWalk()
    {
        var currPos = startPos;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < iterations; i++)
        {
            var path = RandomWalk.RandomWalkGen(currPos, walkLength, chance);
            floorPositions.UnionWith(path);
            if (randomEachIteration)
                currPos = floorPositions.ElementAt(Random.Range(0,floorPositions.Count));
        }
        
        return floorPositions;
    }
}
