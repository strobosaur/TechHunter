using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDlevel : MonoBehaviour
{
    public static HUDlevel instance;
    public TMP_Text HPmax, HPcur, scraps, tech, timer;

    public System.Action onHPchanged;
    public System.Action onScrapsChanged;
    public System.Action onTechChanged;

    void OnEnable()
    {
        instance = this;

        onHPchanged += UpdateHP;
        onScrapsChanged += UpdateScraps;
        onTechChanged += UpdateTech;

        Inventory.instance.onScrapsChanged += UpdateScraps;
        Inventory.instance.onTechChanged += UpdateTech;
    }

    void OnDisable()
    {
        onHPchanged -= UpdateHP;
        onScrapsChanged -= UpdateScraps;
        onTechChanged -= UpdateTech;

        Inventory.instance.onScrapsChanged -= UpdateScraps;
        Inventory.instance.onTechChanged -= UpdateTech;
    }

    public void UpdateHUD()
    {
        UpdateHP();
        UpdateScraps();
        UpdateTech();
    }

    private void UpdateHP()
    {
        HPmax.text = Mathf.CeilToInt(PlayerManager.instance.playerStats.HPmax.GetValue()).ToString();
        HPcur.text = Mathf.CeilToInt(PlayerManager.instance.playerStats.HPcur).ToString();
    }

    private void UpdateScraps()
    {
        scraps.text = Inventory.instance.scraps.ToString();
    }

    private void UpdateTech()
    {
        tech.text = Inventory.instance.techUnits.ToString();
    }

    public void UpdateTimer(float timeLeft)
    {        
        if (timeLeft > 0)
        {
            System.TimeSpan result = System.TimeSpan.FromSeconds(timeLeft);
            System.DateTime actualResult = System.DateTime.MinValue.Add(result);
            timer.text = actualResult.ToString("mm:ss:ff");
        } else {
            timer.text = "LEVEL WON";
        }
    }

    public void UpdatePylons(int current, int target)
    {
        timer.text = current.ToString() + " / " + target.ToString() + " Pylons Powered";
    }
}
