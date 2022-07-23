using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNeighborhoodGraph
{
    public class RngVector
    {
        public Vector2 position;
        public List<Vector2> connections;

        public void AddConnection(Vector2 input)
        {
            connections.Add(input);
        }
    }

    public HashSet<Vector2Int> ConvertRngToHash(int[,] input)
    {
        HashSet<Vector2Int> result = new HashSet<Vector2Int>();
        for (int j = 0; j < input.GetLength(1); j++)
        {
            for (int i = 0; i < input.GetLength(0); i++)
            {
                if (input[i,j] == 1)
                    result.Add(new Vector2Int(i,j));
            }            
        }

        return result;
    }

    public int[,] RNGgen(int size, int coords, float minDist, int fillWith, float border, bool circleArea = false)
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
                    SetCircle2DArr(outgrid, (int)a.x, (int)a.y, Random.Range(3f,8f), 1);
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
            if(!((v.x >= grid.GetLength(0))
            || (v.x < 0)
            || (v.y >= grid.GetLength(1))
            || (v.y < 0)))
                grid[v.x,v.y] = fillValue;
        }
    }
}
