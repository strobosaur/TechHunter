using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechPylon : Interactable
{
    public GameObject pylonProp;
    public GameObject pylonText;

    void OnEnable()
    {
        objectName = "Begin Tech Excavation";
        pylonProp = GameObject.Find("TechPylonProp");
        pylonText = GameObject.Find("TechPylonText");
    }

    public override void Interact()
    {
    }
}
