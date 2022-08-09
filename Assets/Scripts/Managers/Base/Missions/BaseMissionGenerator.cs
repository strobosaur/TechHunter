using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BaseMissionGenerator
{
    private static string[] word1list = {"Valley", "Plains", "Field", "Hills", "Forest", "Marsh", "Swamp", "Woods", "Meadow", "Glade", "Grounds", "Steppe", "Graveyard", "Battlefield"};
    private static string[] word2list = {"Death", "Pain", "Fear", "Struggle", "Danger", "Peril", "Sorrow", "Mourning", "Darkness", "Shadow", "Mystery", "Rapture", "Chaos", "Tears", "Blood", "Bones"};

    public static List<BaseMissionItem> GenerateMissions(float difficulty, int count)
    {
        List<BaseMissionItem> missionList = new List<BaseMissionItem>();

        for (int i = 0; i < count; i++)
        {
            string name = GenerateName();
            int itemDifficulty = Mathf.Max(0, Mathf.RoundToInt(difficulty + Random.Range(-2f, 2f * (float)i)));
            int itemSize = Random.Range(1,4) + Mathf.Max(0, Mathf.RoundToInt(Random.Range(0f, difficulty * 3f) / (difficulty * 4f)));

            BaseMissionItem mission = new BaseMissionItem(name, itemDifficulty, itemSize);

            missionList.Add(mission);
        }

        return missionList;
    }

    private static string GenerateName()
    {
        string mapName = "";

        if (Random.value < 0.5f) {
            mapName += word1list[Random.Range(0, word1list.Length)];
            mapName += " of ";
            mapName += word2list[Random.Range(0,word2list.Length)];
        } else {
            mapName += word2list[Random.Range(0,word2list.Length)];
            mapName += " ";
            mapName += word1list[Random.Range(0, word1list.Length)];
        }

        return mapName;
    }
}
