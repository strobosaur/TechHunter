using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPylonProp : MonoBehaviour
{
    public PowerPylon pylonParent;
    public GameObject pylonText;
    public SpriteRenderer textSR;

    private float pylonTextAlphaBase = 0.8f;
    private float pylonTextAlphaMin = 0f;
    private float pylonTextAlpha = 1f;
    private float pylonAlphaRate = 0.025f;

    void OnEnable()
    {
        pylonParent = GetComponentInParent<PowerPylon>();
        pylonText = transform.GetChild(0).gameObject;
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
            col.a = pylonTextAlphaMin + (pylonTextAlphaBase * pylonTextAlpha * Random.Range(0.8f,1.4f));
            textSR.color = col;
        }
    }

    void ActivatePylon()
    {
        if (!pylonParent.isPoweredUp)
            textSR.enabled = true;
    }

    void DeactivatePylon()
    {
        if (!pylonParent.isPoweredUp)
            textSR.enabled = false;
    }

    public void PowerUpPylon()
    {
        textSR.enabled = true;
        pylonTextAlphaMin = 0.3f;
        pylonTextAlphaBase = 0.6f;
    }
}
