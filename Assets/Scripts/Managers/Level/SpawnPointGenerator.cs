using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointGenerator : MonoBehaviour
{
    public static List<GameObject> spawnPoints = new List<GameObject>();
    public static List<PowerPylon> pylonList = new List<PowerPylon>();

    public GameObject spawnPoint;
    public GameObject pylonPrefab;

    void Awake()
    {
        spawnPoints = new List<GameObject>();
    }

    public List<Vector2Int> FindSpawnPoints(Vector2Int startPos, List<Vector2Int> positions, int number, float minDist = 5f, float startPosDist = 0f)
    {
        List<Vector2Int> outList = new List<Vector2Int>();
        List<Vector2Int> outListFinal = new List<Vector2Int>();

        var furthest = FurthestInList(startPos, positions);
        outList.Add(furthest);
        positions.Remove(furthest);

        // FIND SPAWN POINT PLACES AND CREATE OBJECTS
        for (int i = 1; i < number; i++)
        {
            var data = FurthestInListsAverage(startPos, positions, outList, minDist, startPosDist);
            if (data.Item1){
                furthest = data.Item2;
                outList.Add(furthest);
                positions.Remove(furthest);
            }
        }

        // MAKE SURE SPAWN POINTS ARE AT DISTANCE FROM START POSITION
        for (int j = 0; j < outList.Count; j++)
        {
            if (Vector2.Distance(startPos, outList[j]) > startPosDist) {
                outListFinal.Add(outList[j]);
            }
        }

        return outListFinal;
    }

    private Vector2Int FurthestInList(Vector2Int position, List<Vector2Int> list)
    {
        float distance = 0;
        float tempDist;
        int index = 0;
        for (int i = 0; i < list.Count; i++)
        {
            tempDist = Vector2.Distance(position, list[i]);
            if (tempDist > distance) {
                distance = tempDist;
                index = i;
            }
        }

        return list[index];
    }

    private Vector2Int NearestInList(Vector2Int position, List<Vector2Int> list)
    {
        float distance = Mathf.Infinity;
        float tempDist;
        int index = 0;
        for (int i = 0; i < list.Count; i++)
        {
            tempDist = Vector2.Distance(position, list[i]);
            if (tempDist < distance) {
                distance = tempDist;
                index = i;
            }
        }

        return list[index];
    }

    private (bool, Vector2Int) FurthestInListsAverage(Vector2Int position, List<Vector2Int> list1, List<Vector2Int> list2, float minDist = 5f, float startPosDist = 0f)
    {
        float distance = 0;
        float tempDist1;
        float tempDist2;
        int index = 0;
        bool found = false;

        for (int i = 0; i < list1.Count; i++)
        {
            tempDist1 = Vector2.Distance(position, list1[i]);

            for (int j = 0; j < list2.Count; j++)
            {
                tempDist2 = Vector2.Distance(list1[i], list2[j]);
                
                if ((((tempDist1 + tempDist2) / 2f) > distance)
                && (tempDist1 > startPosDist) 
                && (tempDist2 > minDist))
                {
                    distance = ((tempDist1 + tempDist2) / 2f);
                    index = i;
                    found = true;
                }
            }
        }

        return (found, list1[index]);
    }

    public bool MakeSpawnPoints(Vector2Int startPos, List<Vector2Int> positions, int number, float minDist = 5f, float startPosDist = 0f)
    {
        List<Vector2Int> list = FindSpawnPoints(startPos, positions, number, minDist, startPosDist);
        foreach (var position in list)
        {
            var ob = Instantiate(spawnPoint);
            ob.transform.position = (Vector2)position;

            spawnPoints.Add(ob);

            var ob2 = Instantiate(pylonPrefab, ob.transform);
            pylonList.Add(ob2.GetComponent<PowerPylon>());
        }

        if (list.Count > 0) {
            return true;
        } else {
            return false;
        }
    }

    public void DeleteAllSpawnPoints()
    {
        foreach (var item in spawnPoints)
        {
            Destroy(item.transform.GetChild(0).gameObject);
            Destroy(item);
        }

        pylonList.Clear();
        spawnPoints.Clear();
    }
}
