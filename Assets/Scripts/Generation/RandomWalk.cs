using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RandomWalk
{
    public static HashSet<Vector2Int> RandomWalkGen(Vector2Int origin, int steps, float chance = 1f)
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

    public static HashSet<Vector2Int> CorridorFirstGeneration(int corrLen, int corrCount, int walkLen, float roomChance = 0.8f)
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRooms = new HashSet<Vector2Int>();

        floorPositions = CreateCorridors(floorPositions, potentialRooms, Vector2Int.zero, corrLen, corrCount);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRooms, walkLen, roomChance);

        floorPositions.UnionWith(roomPositions);

        return floorPositions;
    }

    private static HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRooms, int walkLen, float roomChance)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomCount = Mathf.RoundToInt(potentialRooms.Count * roomChance);
        
        List<Vector2Int> roomsToCreate = potentialRooms.OrderBy(x => System.Guid.NewGuid()).Take(roomCount).ToList();
        foreach (var position in roomsToCreate)
        {
            var roomFloor = RandomWalkGen(position, walkLen);
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;
    }

    private static HashSet<Vector2Int> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRooms, Vector2Int origin, int corrLen, int corrCount)
    {
        var currentPosition = origin;
        potentialRooms.Add(currentPosition);

        for (int i = 0; i < corrCount; i++)
        {
            var corridor = RandomWalkCorridor(currentPosition, corrLen);
            currentPosition = corridor[corridor.Count - 1];
            floorPositions.UnionWith(corridor);
            potentialRooms.Add(currentPosition);
        }

        return floorPositions;
    }

    // BINARY SPACE PARTITIONING
    public static List<BoundsInt> BSPgen(BoundsInt splitSpace, int minW, int minH)
    {
        Queue<BoundsInt> roomQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomList = new List<BoundsInt>();
        roomQueue.Enqueue(splitSpace);
        while(roomQueue.Count > 0)
        {
            var room = roomQueue.Dequeue();
            if ((room.size.y >= minH) && (room.size.x >= minW))
            {
                if (Random.value < 0.5f)
                {
                    if (room.size.y >= minH * 2)
                    {
                        SplitHorizontally(minH,roomQueue,room);
                    } 
                    else if (room.size.x >= minW * 2) 
                    {
                        SplitVertically(minW,roomQueue,room);
                    } else if (room.size.x >= minW && room.size.y >= minH) {
                        roomList.Add(room);
                    }
                } else {
                    if (room.size.x >= minW * 2) 
                    {
                        SplitVertically(minW,roomQueue,room);
                    } 
                    else if (room.size.y >= minH * 2)
                    {
                        SplitHorizontally(minH,roomQueue,room);
                    } 
                    else if (room.size.x >= minW && room.size.y >= minH) {
                        roomList.Add(room);
                    }
                }
            }
        }

        return roomList;
    }

    private static void SplitVertically(int minW, Queue<BoundsInt> roomQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.min.y, room.min.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));

        roomQueue.Enqueue(room1);
        roomQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minH, Queue<BoundsInt> roomQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));

        roomQueue.Enqueue(room1);
        roomQueue.Enqueue(room2);
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