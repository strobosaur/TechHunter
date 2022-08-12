using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechPylonProp : MonoBehaviour
{
    public TechPylon pylonParent;
    public GameObject pylonText;
    public SpriteRenderer textSR;

    public float pylonTextAlpha = 1f;
    public float pylonAlphaRate = 0.05f;

    void OnEnable()
    {
        pylonParent = GetComponentInParent<TechPylon>();
        pylonText = GameObject.Find("TechPylonText");
        textSR = pylonText.GetComponent<SpriteRenderer>();
        textSR.enabled = false;

        pylonParent.onInteractable += ActivatePylon;
        pylonParent.onNotInteractable += DeactivatePylon;

        pylonTextAlpha = 1f;
        pylonAlphaRate = 0.025f;
    }

    void OnDisable()
    {
        pylonParent.onInteractable -= ActivatePylon;
        pylonParent.onNotInteractable -= DeactivatePylon;
    }

    void Update()
    {
        if (textSR.enabled)
        {
            // PYLON ALPHA
            pylonTextAlpha -= (pylonAlphaRate);
            if (!((pylonTextAlpha > 0f) && (pylonTextAlpha < 1f)))
                pylonAlphaRate *= -1f;

            Color col = textSR.color;
            col.a = 0.8f * pylonTextAlpha * Random.Range(0.8f,1.4f);
            textSR.color = col;
        }
    }

    void ActivatePylon()
    {
        textSR.enabled = true;
    }

    void DeactivatePylon()
    {
        textSR.enabled = false;
    }
}
