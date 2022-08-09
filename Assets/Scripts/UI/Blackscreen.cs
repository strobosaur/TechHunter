using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Blackscreen : MonoBehaviour
{
    public Image blackscreen;
    public TMP_Text interactableMessage;
    Color bsColor;

    float bsFadeTime = 2f;
    float bsFadeCounter = 0f;
    bool isFading = false;
    bool fadeToBlack = false;

    // ON FADED EVENTS
    public System.Action OnBlackScreenBlack;
    public System.Action OnBlackScreenClear;

    // AWAKE
    void Awake()
    {
        blackscreen = GetComponent<Image>();
    }

    // UPDATE
    void Update()
    {
        if (isFading) {
            BlackScreenFade(fadeToBlack);
        } else if (interactableMessage.enabled == false) {
            interactableMessage.enabled = true;
        }
    }

    // BLACKSCREEN FADE
    void BlackScreenFade(bool toBlack)
    {
        // IF TIMER IS STILL TICKING
        if (bsFadeCounter < bsFadeTime)
        {
            // UPDATE TIMER
            bsFadeCounter = Globals.Approach(bsFadeCounter, bsFadeTime, Time.deltaTime);

            // FADE OUT OR FADE IN?
            if (toBlack) bsColor.a = Globals.EaseInSine((bsFadeCounter / bsFadeTime), 0f, 1f, 1f);
            else bsColor.a = Globals.EaseInSine((1f - (bsFadeCounter / bsFadeTime)), 0f, 1f, 1f);

            // SET SCREEN COLOR
            blackscreen.color = bsColor;

        } else if (!toBlack) {

            // SCREEN HAS FADED TO CLEAR
            blackscreen.enabled = false;
            ResetBlackScreen();
            OnBlackScreenClear?.Invoke();

        } else {

            // SCREEN HAS FADED TO BLACK
            ResetBlackScreen();
            OnBlackScreenBlack?.Invoke();
        }
    }

    // START FADER
    public void StartBlackScreenFade(bool toBlack = true)
    {
        isFading = true;
        fadeToBlack = toBlack;
        bsFadeCounter = 0f;
        interactableMessage.enabled = false;

        // FADE TO BLACK
        if (toBlack) {
            blackscreen.enabled = true;
            bsColor = blackscreen.color;
            bsColor.a = 0f;
            blackscreen.color = bsColor;

        // FADE TO CLEAR
        } else {
            blackscreen.enabled = true;
            bsColor = blackscreen.color;
            bsColor.a = 1f;
            blackscreen.color = bsColor;
        }
    }

    // RESET BLACKSCREEN
    private void ResetBlackScreen()
    {
        isFading = false;
        bsFadeCounter = 0f;
    }
}
