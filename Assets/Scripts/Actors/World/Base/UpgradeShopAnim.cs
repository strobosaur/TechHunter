using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShopAnim : MonoBehaviour
{
    public float currentAlpha = 0f;
    public float targetAlpha = 0f;
    public SpriteRenderer sr;
    Color col = new Color(0.75f,1f,1f,1f);//Color.HSVToRGB(180f/255f, 0.25f, 1f);

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (currentAlpha != targetAlpha)
        {
            currentAlpha = Globals.Approach(currentAlpha, targetAlpha, 0.01f);
        }

        col.a = currentAlpha * (1f - (DisplayManager.instance.textAlpha1 * 0.3f)) * (1f - (DisplayManager.instance.textAlpha2 * 0.15f));
        sr.color = col;
    }

}
