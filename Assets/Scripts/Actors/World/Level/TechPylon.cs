using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechPylon : Interactable
{
    public GameObject pylonProp;
    public GameObject pylonText;
    public bool canActivate = false;

    void OnEnable()
    {
        canActivate = false;
        objectName = "Begin Tech Excavation";
        pylonProp = GameObject.Find("TechPylonProp");
        pylonText = GameObject.Find("TechPylonText");
    }

    void Start()
    {
        CurrentLevelManager.instance.onPylonCountReached += CanActivate;
    }

    void OnDisable()
    {
        CurrentLevelManager.instance.onPylonCountReached -= CanActivate;
    }

    public override void Interact()
    {
        if (canActivate)
        {
            // PLAY SOUND EFFECT
            AudioManager.instance.Play("menu_choice");

            CurrentLevelManager.instance.LevelWin();
        } else {

            // PLAY SOUND EFFECT
            AudioManager.instance.Play("menu_blip");
        }
    }

    public void CanActivate()
    {
        CurrentLevelManager.instance.onPylonCountReached -= CanActivate;
        canActivate = true;
    }
}
