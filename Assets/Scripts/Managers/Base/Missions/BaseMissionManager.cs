using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseMissionManager : MonoBehaviour
{
    public CanvasGroup missionGroup;

    public GameObject locationTextPF, difficultyTextPF, sizeTextPF;
    public List<TMP_Text> missionTextLocList = new List<TMP_Text>();
    public List<TMP_Text> missionTextDifList = new List<TMP_Text>();
    public List<TMP_Text> missionTextSizeList = new List<TMP_Text>();
    public List<BaseMissionItem> missionList = new List<BaseMissionItem>();

    void Awake()
    {
        missionGroup = GameObject.Find("MissionMenuContainer").GetComponent<CanvasGroup>();
        missionList.Add(new BaseMissionItem("Mission 1", 2, 3));
        missionList.Add(new BaseMissionItem("Mission 2", 1, 5));
        missionList.Add(new BaseMissionItem("Mission 3", 3, 2));

        CreateTextLists();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateTextLists()
    {
        for (int i = 0; i < missionList.Count; i++)
        {
            var txt1 = Instantiate(locationTextPF, missionGroup.transform);
            txt1.GetComponent<TMP_Text>().text = missionList[i].name;
            
            var txt2 = Instantiate(difficultyTextPF, missionGroup.transform);
            txt2.GetComponent<TMP_Text>().text = missionList[i].difficulty.ToString();
            
            var txt3 = Instantiate(sizeTextPF, missionGroup.transform);
            txt3.GetComponent<TMP_Text>().text = missionList[i].size.ToString();

            txt1.transform.position += new Vector3(0,-16f * i,0);
            txt1.GetComponent<MenuText>().SetStartPos(txt1.transform.position);
            txt2.transform.position += new Vector3(0,-16f * i,0);
            txt2.GetComponent<MenuText>().SetStartPos(txt2.transform.position);
            txt3.transform.position += new Vector3(0,-16f * i,0);
            txt3.GetComponent<MenuText>().SetStartPos(txt3.transform.position);

            missionTextLocList.Add(txt1.GetComponent<TMP_Text>());
            missionTextDifList.Add(txt2.GetComponent<TMP_Text>());
            missionTextSizeList.Add(txt3.GetComponent<TMP_Text>());
        }
    }

    void GenerateMissions(int difficulty)
    {

    }

    public void ToggleMissionMenu(bool show = true)
    {
        if (show) {
            missionGroup.alpha = 1f;
            missionGroup.interactable = true;
        } else {
            missionGroup.alpha = 0f;
            missionGroup.interactable = false;
        }
    }
}
