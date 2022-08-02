using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;

    Inventory inventory;
    Equipment[] currentEquipment;

    // CALLBACK METHOD
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;

        inventory = Inventory.instance;
    }

    void Start()
    {
        int equipSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[equipSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = null;

        // CHECK FOR ALREADY EQUIPPED ITEM IN SLOT
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.AddItem(oldItem);
        }

        // FIRE CALLBACK METHODS
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        // EQUIP NEW ITEM
        currentEquipment[slotIndex] = newItem;
    }

    public void Unequip(int slotIndex)
    {
        // CHECK FOR ITEM IN SLOT
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.AddItem(oldItem);

            currentEquipment[slotIndex] = null;

            // FIRE CALLBACK METHODS
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }
}
