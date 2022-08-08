using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUpgradeShop : Interactable
{
    BaseManager manager;
    UpgradeShopAnim upgAnim;

    void Awake()
    {
        manager = GameObject.Find("BaseManager").GetComponent<BaseManager>();
        upgAnim = GameObject.Find("UpgradeShopAnim").GetComponent<UpgradeShopAnim>();
        objectName = "Upgrade Shop";
    }

    void Update()
    {
        if (isInteractable)
        {
            upgAnim.targetAlpha = 0.7f;
        } else if (!isInteractable) {
            upgAnim.targetAlpha = 0f;
        }
    }

    public override void Interact()
    {
        manager.stateMachine.ChangeState(manager.stateUpgrade);
    }
}
