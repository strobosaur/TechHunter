using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    // DATE, MISSIONS, DIFFICULTY, TECH, KILLS, SCORE
    public List<(System.DateTime, int, int, int, int, int)> highscoreList = new List<(System.DateTime, int, int, int, int, int)>();

    public TMP_Text hsText, goText;
    public TMP_Text[] hsTexts;

    public int totalScore;

    void Awake()
    {
        instance = this;
    }

    // ON ENABLE
    void OnEnable()
    {
        // DISABLE ALL ELEMENTS
        ToggleHighscoreGUI(false);
        LoadGame();
    }

    // ON DISABLE
    void OnDisable()
    {
    }

    void Start()
    {
    }

    // CALCULATE FINAL SCORE
    public int CalculateFinalScore()
    {
        float tempScore = 0;

        float shellKills = Inventory.instance.killDict["Shell"];
        float germKills = Inventory.instance.killDict["Germinite"];
        float glandKills = Inventory.instance.killDict["Gland"];

        float totalScraps = Inventory.instance.totalScraps;
        float totalTech = Inventory.instance.totalTechUnits;
        float totalPylonsPoweredOn = Inventory.instance.totalPylonsPoweredOn;

        float missionsWon = Inventory.instance.missionsWon;
        float difficulty = LevelManager.instance.difficulty;

        tempScore += shellKills * 2;
        tempScore += germKills * 2.5f;
        tempScore += glandKills * 4f;

        tempScore += totalScraps * 2;
        tempScore += totalTech * 100f;
        tempScore += totalPylonsPoweredOn * 25f;
        tempScore += missionsWon * 250f;

        tempScore += difficulty * 100f;

        totalScore = Mathf.RoundToInt(tempScore);
        return Mathf.RoundToInt(tempScore);
    }

    // DISABLE ALL GUI
    public void ToggleHighscoreGUI(bool enable = true)
    {
        hsText.gameObject.SetActive(enable);
        hsText.enabled = enable;

        foreach (var text in hsTexts)
        {
            text.gameObject.SetActive(enable);
            text.enabled = enable;
        }
    }

    // CREATE HIGHSCORE ENTRY
    public void CreateHighscoreEntry()
    {
        // ADD HIGH SCORE ENTRY
        System.DateTime date = System.DateTime.Now;
        int missionsWon = Inventory.instance.missionsWon;
        int difficulty = Mathf.RoundToInt(LevelManager.instance.difficulty);
        int totalTech = Inventory.instance.totalTechUnits;
        int kills = Inventory.instance.kills;
        int score = CalculateFinalScore();

        highscoreList.Add((date,missionsWon,difficulty,totalTech,kills,score));
        highscoreList = highscoreList.OrderByDescending(score => score.Item6).ToList();

        SaveGame();
    }

    // DISPLAY HIGHSCORES
    public void DisplayHighscores(bool show = true)
    {
        if (show)
        {
            goText.gameObject.SetActive(false);
            goText.enabled = false;

            ToggleHighscoreGUI(true);

            float hueMod = 1f;

            // ENABLE SCORE TEXT OBJECTS
            foreach (var t in hsTexts) { t.GetComponent<TMP_Text>().enabled = true; }

            if (highscoreList.Count > 1) {
                float diff = highscoreList[0].Item6 - highscoreList[Mathf.Min(4, (highscoreList.Count - 1))].Item6;
                hueMod = (diff / (highscoreList.Count + 1)) / diff;
            }

            for (int i = 0; i < Mathf.Min(5, highscoreList.Count); i++)
            {
                TMP_Text hs = hsTexts[i].GetComponent<TMP_Text>();
                hs.color = Color.HSVToRGB((0.25f + (hueMod * i)) % 1f, 0.25f, 1f);

                hs.text = highscoreList[i].Item1.ToString("yyyy/MM/dd - HH:mm:ss") + " | " 
                        + "Missions: " + highscoreList[i].Item2.ToString() + " | "
                        + "Difficulty: " + highscoreList[i].Item3.ToString() + " | "
                        + "Tech Units: " + highscoreList[i].Item4.ToString() + " | "
                        + "Kills: " + highscoreList[i].Item5 + " | "
                        + "Score: " + highscoreList[i].Item6 + " pts.";
            }
        } else {
            // DISABLE SCORE TEXT OBJECTS
            ToggleHighscoreGUI(false);
        }
    }

    // LOAD STATE SCENE MANAGEMENT
    public void SaveState()
    {
        string s = "";
        s += totalScore.ToString() + ";";
        s += Inventory.instance.missionsWon.ToString() + ";";
        s += LevelManager.instance.difficulty.ToString();
        s += Inventory.instance.totalTechUnits.ToString();
        s += Inventory.instance.kills.ToString();
        s += LevelManager.instance.difficulty.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }

    // LOAD STATE SCENE MANAGEMENT
    public void SaveGame()
    {
        // CREATE NEW SAVE STRING
        string s = "";
        foreach (var item in highscoreList)
        {
            s += item.Item1.ToString("O") + ";";
            s += item.Item2.ToString() + ";";
            s += item.Item3.ToString() + ";";
            s += item.Item4.ToString() + ";";
            s += item.Item5.ToString() + ";";
            s += item.Item6.ToString() + ";";
            s += "|";
        }

        PlayerPrefs.SetString("SaveGame", s);
    }

    // LOAD GAME
    public void LoadGame()
    {
        if (!PlayerPrefs.HasKey("SaveGame")) {
            return;
        } else {

            highscoreList.Clear();

            // SPLIT STRING ROWS
            string[] rows = PlayerPrefs.GetString("SaveGame").Split("|", System.StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in rows)
            {
                // CREATE NEW ROW DATA
                string[] row = item.Split(";", System.StringSplitOptions.RemoveEmptyEntries);

                System.DateTime date = System.DateTime.ParseExact(row[0], "O", System.Globalization.CultureInfo.InvariantCulture);
                int missionsWon = int.Parse(row[1]);
                int difficulty = int.Parse(row[2]);
                int tech = int.Parse(row[3]);
                int kills = int.Parse(row[4]);
                int score = int.Parse(row[5]);

                // ADD TO LIST
                highscoreList.Add((date,missionsWon,difficulty,tech,kills,score));
            }

            // ORDER LIST BY DESCENDING
            highscoreList = highscoreList.OrderByDescending(score => score.Item6).ToList();
        }
    }
}
