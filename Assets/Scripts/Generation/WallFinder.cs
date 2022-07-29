using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class WallFinder
{
    public static HashSet<Vector2Int> MakeWalls(IEnumerable<Vector2Int> floorPositions, TileManager tileManager)
    {
        var wallPositions = FindWallsCardinal(floorPositions, Direction2D.dirList);
        foreach (var position in wallPositions)
        {
            tileManager.PaintSingleWall(position);
        }

        return wallPositions;
    }

    private static HashSet<Vector2Int> FindWallsCardinal(IEnumerable<Vector2Int> floorPositions, List<Vector2Int> dirList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            foreach (var direction in dirList)
            {
                var neighbor =  position + direction;
                if(!floorPositions.Contains(neighbor)) {
                    wallPositions.Add(neighbor);
                }
            }
        }

        return wallPositions;
    }
}
