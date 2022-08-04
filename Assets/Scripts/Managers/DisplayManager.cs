using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManager : MonoBehaviour
{
    public static DisplayManager instance;

    public float textAlpha1 = 1f;
    public float textAlpha2 = 1f;
    public float textAlphaRate1 = 0.00125f;
    public float textAlphaRate2 = 0.01f;

    // AWAKE
    void Awake()
    {
        textAlphaRate1 = 0.004f;
        textAlphaRate2 = 0.05f;
        instance = this;
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
        textAlpha1 -= (textAlphaRate1);
        if (!((textAlpha1 > 0f) && (textAlpha1 < 1f)))
            textAlphaRate1 *= -1f;
        //textAlpha1 = Mathf.Clamp(textAlpha1, 0f, 1f);

        // TEXT ALPHA 2
        textAlpha2 -= (textAlphaRate2);
        if (!((textAlpha2 > 0f) && (textAlpha2 < 1f)))
            textAlphaRate2 *= -1f;
        //textAlpha2 = Mathf.Clamp(textAlpha2, 0f, 1f);
    }
}
