using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private List<TileBase> floorTiles;

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
        PaintFloorTiles(floorPos);
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

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTiles);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, List<TileBase> tiles)
    {
        foreach (var position in positions)
        {
            TileBase tile;
            if (Random.Range(0f,1f) < 0.2f)
                tile = tiles[Random.Range(1,tiles.Count)];
            else
                tile = tiles[0];
            PaintSingleTile(tilemap, tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePos = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePos, tile);
    }

    public class RngVector
    {
        public Vector2 position;
        public List<Vector2> connections;

        public void AddConnection(Vector2 input)
        {
            connections.Add(input);
        }
    }

    public int[,] RNGgen(int size, int coords, float minDist, int fillWith, float border, bool circleArea)
    {
        // INPUT
        int gw = size;
        int gh = size;

        int[,] outgrid = new int[gw, gh];
        
        // LOCAL VARS                
        int xpad = Mathf.RoundToInt(border);
        int ypad = Mathf.RoundToInt(border);
        
        List<Vector2> coordList = new List<Vector2>();
        
        Vector2 a = Vector2.zero;
        Vector2 b = Vector2.zero;
        Vector2 c = Vector2.zero;
        Vector2 n = Vector2.zero;
        
        float ab = 0;
        float ac = 0;
        float bc = 0;
        
        bool connect = false;
        
        // RANDOM NEIGHBORHOOD GRAPH
        // ==============================
        // LOOP I: Spread random points
        
        for (int i = coords; i > 0; i--)
        {
            // SQUARE BORDER
            if (!circleArea)
                c = new Vector2(Random.Range(xpad, gw - xpad), Random.Range(ypad, gh-ypad));
            else {
            
                // CIRCLE BORDER
                c = Random.insideUnitCircle * Mathf.Min((gw / 2) - xpad, (gh / 2) - ypad);
            }
            
            // ADD COORD TO LIST
            coordList.Add(c);
        }
        
        // LOOP II: List all A&B distances
        
        for(int i = 0; i < coordList.Count; i++){
            for(var j = i+1; j < coordList.Count; j++){            
                
                // GET DISTANCE BETWEEN EACH VECTOR
                a = coordList[i];
                b = coordList[j];
                ab = Vector2.Distance(a, b);
                
                // CHECK AGAINST MIN DISTANCE
                if (ab < minDist) continue; 
                
                // OTHERWISE CONNECT
                connect = true;
                
                // LOOP III: Compare every A&B pair with the others                
                for(int k = 0; k < coordList.Count; k++)
                {
                
                    c = coordList[k];
                    
                    if ((c == b) || (c == a)) continue;
                    
                    ac = Vector2.Distance(a, c);
                    bc = Vector2.Distance(b, c);
                    
                    if ((ac < minDist) || ( bc < minDist)) continue;
                    
                    // No connection if C is closer to both A & B
                    if ((ac < ab) && (bc < ab))
                    {
                        connect = false;
                        break;
                    }
                }
                
                // Make grid line between connected points
                if (connect)
                {
                    outgrid[Mathf.RoundToInt(a.x), Mathf.RoundToInt(a.y)] = fillWith;
                    outgrid[Mathf.RoundToInt(b.x), Mathf.RoundToInt(b.y)] = fillWith;
                    n = a;

                    while (Vector2.Distance(n,b) > 0.1f)
                    {
                        n = Vector2.MoveTowards(n,b,0.5f);
                        outgrid[Mathf.RoundToInt(n.x), Mathf.RoundToInt(n.y)] = fillWith;
                    }
                }
            }
        }

        return outgrid;
    }

    // SET CIRCLE IN 2D ARRAY
    public void SetCircle2DArr(int[,] grid, int x, int y, float radius, int fillValue)
    {
        List<Vector2Int> offsets = new List<Vector2Int>();
        int iRadius = Mathf.RoundToInt(radius);
        int threshold = Mathf.RoundToInt(radius * radius);

        for (int i = 1; i < iRadius; i++)
        {
            for (int j = 1; j < iRadius; j++)
            {
                if ((i*i + j*j) < threshold)
                {
                    offsets.Add(new Vector2Int(x+i, y+j));
                    offsets.Add(new Vector2Int(x+(-i), y+j));
                    offsets.Add(new Vector2Int(x+i, y+(-j)));
                    offsets.Add(new Vector2Int(x+(-i), y+(-j)));
                }
            }
        }

        foreach (Vector2Int v in offsets)
        {
            grid[v.x,v.y] = fillValue;
        }
    }
}
