using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public List<MenuText> menuTexts;
    public int currentIndex = 0;

    // UPDATE
    void Update()
    {
        if (InputManager.input.down.WasPressedThisFrame()) currentIndex = (((currentIndex + menuTexts.Count) + 1) % menuTexts.Count);
        if (InputManager.input.up.WasPressedThisFrame()) currentIndex = (((currentIndex + menuTexts.Count) - 1) % menuTexts.Count);
        UpdateTexts(currentIndex);
    }

    // UPDATE ALL TEXTS
    void UpdateTexts(int index)
    {
        for (int i = 0; i < menuTexts.Count; i++)
        {
            if (i == index) {
                menuTexts[i].isChosen = true;
            } else {
                menuTexts[i].isChosen = false;
            }
        }
    }
}
