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

    // BSP PARAMETERS
    public int BSPwidth = 128;
    public int BSPheight = 128;
    public int BSProomWidth = 16;
    public int BSProomHeight = 16;
    public int BSPpadding = 2;
    public int BSPsteps = 25;
    public int BSPiterations = 5;

    void Start()
    {
        //GenerateMap();
        //GenerateMapCF();
        GenerateMapRNG();
        //GenerateMapBSP_RW();
    }

    void Update()
    {
        if (InputManager.input.X.WasPressedThisFrame())
        {
            //GenerateMapCF();
            GenerateMapRNG();
            //GenerateMapBSP_RW();
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

    // GENERATE MAP WITH CELLULAR AUTOMATA + RANDOM NEIGHBORHOOD GRAPH
    public void GenerateMapRNG()
    {
        tileManager.ClearTiles();

        var rngData = rngGen.RNGgen(RNGsize,RNGpoints,RNGminDist,1,8);
        int[,] rngGrid = rngData.Item1;
        var rngPoints = rngData.Item2;

        rngGrid = rngGen.AddCardinalDirsArr(rngGrid);
        rngGrid = rngGen.CA_RNG(rngGrid, CAlivechance);

        HashSet<Vector2Int> floorPos = rngGen.ConvertRngToHash(rngGrid);
        tileManager.PaintFloorTiles(floorPos);
        WallFinder.MakeWalls(floorPos, tileManager);

        GameObject.Find(Globals.G_PLAYERNAME).transform.position = new Vector3(rngGen.startPos.x,rngGen.startPos.y,0f); //* (1f / Globals.G_CELLSIZE);
        InvokeRepeating("UpdateAstar", 0.5f, 5f);
    }

    public void UpdateAstar()
    {
        EnemyManager.instance.astar.Scan();        
    }

    public void GenerateMapBSP_RW()
    {
        tileManager.ClearTiles();
        List<BoundsInt> bounds = RandomWalk.BSPgen(new BoundsInt(
            new Vector3Int(-(BSPwidth / 2), -(BSPheight / 2), 0),
            new Vector3Int((BSPwidth / 2), (BSPheight / 2), 0)), BSProomWidth, BSProomHeight);

        List<Vector2Int> roomPositions = RandomWalk.FindPotentialRoomPos(bounds);
        HashSet<Vector2Int> floorTiles = RandomWalk.MakeRoomsBSP(roomPositions, bounds, BSPsteps, BSPiterations, BSPpadding);

        tileManager.PaintFloorTiles(floorTiles);
        WallFinder.MakeWalls(floorTiles, tileManager);
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
