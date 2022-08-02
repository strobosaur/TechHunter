using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    public int armorModifier;
    public int damageModifier;

    public override void UseItem()
    {
        base.UseItem();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }
}

public enum EquipmentSlot {
    head,
    chest,
    legs,
    weapon
}
