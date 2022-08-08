using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionChooser : Interactable
{
    BaseManager manager;

    void Awake()
    {
        manager = GameObject.Find("BaseManager").GetComponent<BaseManager>();
        objectName = "Choose Mission";
    }

    public override void Interact()
    {
        manager.stateMachine.ChangeState(manager.stateMission);
    }
}
