using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public TileManager tileManager;
    public RandomNeighborhoodGraph rngGen = new RandomNeighborhoodGraph();
    public Vector2Int startPos = Vector2Int.zero;
    public float chance = 0.5f;
    public int iterations = 25;
    public int walkLength = 100;
    public bool randomEachIteration = true;

    public int RNGsize = 128;
    public int RNGpoints = 24;
    public float RNGminDist = 10f;

    public float CAlivechance = 0.4f;

    // CORRIDORS FIRST
    public int corrLen = 14;
    public int corrCount = 10;

    void Awake()
    {
        //GenerateMap();
        //GenerateMapCF();
        GenerateMapRNG();
    }

    void Update()
    {
        if (InputManager.input.X.WasPressedThisFrame())
        {
            //GenerateMapCF();
            GenerateMapRNG();
        }
    }

    public void GenerateMap()
    {
        tileManager.ClearTiles();
        HashSet<Vector2Int> floorPos = RunRandomWalk();
        floorPos = AddCardinalDirs(floorPos);
        tileManager.PaintFloorTiles(floorPos);
        WallFinder.MakeWalls(floorPos, tileManager);
    }

    public void GenerateMapCF()
    {
        tileManager.ClearTiles();
        HashSet<Vector2Int> floorPos = RandomWalk.CorridorFirstGeneration(corrLen, corrCount, walkLength);
        //floorPos = AddCardinalDirs(floorPos);
        tileManager.PaintFloorTiles(floorPos);
        WallFinder.MakeWalls(floorPos, tileManager);
    }

    public void GenerateMapRNG()
    {
        tileManager.ClearTiles();
        //HashSet<Vector2Int> floorPos = rngGen.ConvertRngToHash(rngGen.CA_RNG(rngGen.RNGgen(RNGsize,RNGpoints,RNGminDist,1,2), CAlivechance));
        int[,] rngGrid = rngGen.RNGgen(RNGsize,RNGpoints,RNGminDist,1,2);
        rngGrid = rngGen.AddCardinalDirsArr(rngGrid);
        rngGrid = rngGen.CA_RNG(rngGrid, CAlivechance);
        HashSet<Vector2Int> floorPos = rngGen.ConvertRngToHash(rngGrid);
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

    public HashSet<Vector2Int> RunRandomWalk()
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
