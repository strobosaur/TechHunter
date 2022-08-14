using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameLogoScreen : MonoBehaviour
{
    private float startTime;
    public float minAlpha;
    public float maxAlpha;
    private float alphaRate;
    public float textAlpha;
    private float duration;
    public TMP_Text text;

    public System.Action onLogoFadeIn;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        startTime = Time.time;
        textAlpha = 0f;
        alphaRate = 0.0025f;
        minAlpha = 0f;
        maxAlpha = 0f;
    }

    void Update()
    {
        //text.alpha = textAlpha * Random.Range(0.25f,4f);

        if (maxAlpha < 1f)
        {
            maxAlpha = Globals.Approach(maxAlpha, 1f, alphaRate);
        } else if (minAlpha < 1f) {
            minAlpha = Globals.Approach(minAlpha, 1f, alphaRate);
        } else if ((maxAlpha >= 1f) && (minAlpha >= 1f)) {
            onLogoFadeIn?.Invoke();
        }

        textAlpha = Random.Range(minAlpha, maxAlpha);
        text.alpha = textAlpha;
    }

    public void LogoFadeIn()
    {
        text = GetComponent<TMP_Text>();
        startTime = Time.time;
        textAlpha = 0f;
        alphaRate = 0.0025f;
        minAlpha = 0f;
        maxAlpha = 0f;
    }
}
