using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDlevel : MonoBehaviour
{
    public static HUDlevel instance;
    public TMP_Text HPmax, HPcur, scraps, tech;

    public System.Action onHPchanged;
    public System.Action onScrapsChanged;
    public System.Action onTechChanged;

    void OnEnable()
    {
        instance = this;

        onHPchanged += UpdateHP;
        onScrapsChanged += UpdateScraps;
        onTechChanged += UpdateTech;
    }

    void OnDisable()
    {
        onHPchanged -= UpdateHP;
        onScrapsChanged -= UpdateScraps;
        onTechChanged -= UpdateTech;
    }

    public void UpdateHUD()
    {
        UpdateHP();
        UpdateScraps();
        UpdateTech();
    }

    private void UpdateHP()
    {
        HPmax.text = Mathf.RoundToInt(PlayerManager.instance.playerStats.HPmax.GetValue()).ToString();
        HPcur.text = Mathf.RoundToInt(PlayerManager.instance.playerStats.HPcur).ToString();
    }

    private void UpdateScraps()
    {
        scraps.text = Inventory.instance.scraps.ToString();
    }

    private void UpdateTech()
    {
        tech.text = Inventory.instance.techUnits.ToString();
    }
}
