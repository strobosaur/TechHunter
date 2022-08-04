using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManager : MonoBehaviour
{
    public float textAlpha1 = 1f;
    public float textAlpha2 = 1f;
    public float textAlphaRate1 = 0.0125f;
    public float textAlphaRate2 = 0.1f;

    // AWAKE
    void Awake()
    {
        //Application.targetFrameRate = 60;
    }

    // UPDATE
    void Update()
    {
        // UPDATE TEXT ALPHA
        UpdateTextAlpha();
    }

    // UPDATE TEXT ALPHA
    private void UpdateTextAlpha()
    {
        // TEXT ALPHA 1
        if ((textAlpha1 > 0f) || (textAlpha1 < 1f)) textAlpha1 -= (textAlphaRate1 * Time.deltaTime);
        else textAlphaRate1 *= -1f;
        textAlpha1 = Mathf.Clamp(textAlpha1, 0f, 1f);

        // TEXT ALPHA 2
        if ((textAlpha2 > 0f) || (textAlpha2 < 1f)) textAlpha2 -= (textAlphaRate2 * Time.deltaTime);
        else textAlphaRate2 *= -1f;
        textAlpha2 = Mathf.Clamp(textAlpha1, 0f, 1f);
    }
}
