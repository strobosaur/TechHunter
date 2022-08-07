using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUpgradeShop : Interactable
{
    BaseManager manager;

    void Awake()
    {
        manager = GameObject.Find("BaseManager").GetComponent<BaseManager>();
        objectName = "Upgrade Shop";
    }

    public override void Interact()
    {
        manager.stateMachine.ChangeState(manager.stateUpgrade);
    }
}
