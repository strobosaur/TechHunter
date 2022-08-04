using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuText : MonoBehaviour
{
    TMP_Text tmpText;
    Vector2 startPos;
    Vector2 choiceOffset = new Vector2(-8,0);
    Vector2 targetPos;

    public float baseAlpha = 0.5f;
    public float choiceAlpha = 0.9f;
    public float currentAlpha;

    public bool isChosen = false;

    // AWAKE
    void Awake()
    {
        tmpText = GetComponent<TMP_Text>();

        startPos = transform.position;
        targetPos = startPos;
        currentAlpha = baseAlpha;
    }

    // UPDATE
    void Update()
    {
        if (isChosen)
        {
            currentAlpha = Mathf.Lerp(currentAlpha, choiceAlpha, 0.1f);
            transform.position = Vector2.Lerp(transform.position, startPos + choiceOffset, 0.1f);
        } else {
            currentAlpha = Mathf.Lerp(currentAlpha, baseAlpha, 0.1f);
            transform.position = Vector2.Lerp(transform.position, startPos, 0.1f);
        }

        UpdateTextAlpha();
    }

    // TEXT ALPHA
    void UpdateTextAlpha()
    {
        tmpText.alpha = currentAlpha * (1f - (DisplayManager.instance.textAlpha1 * 0.3f)) * (1f - (DisplayManager.instance.textAlpha2 * 0.15f));
    }
}
