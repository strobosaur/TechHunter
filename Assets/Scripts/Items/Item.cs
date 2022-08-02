using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    string itemName = "New Item";

    public virtual void UseItem()
    {

    }

    public void RemoveFromInventory()
    {
        Inventory.instance.RemoveItem(this);
    }
}
