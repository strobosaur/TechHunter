using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // SINGLETON
    public static Inventory instance;

    // ITEM CHANGED EVENTS
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    // RESOURCE CHANGED EVENTS
    public System.Action onScrapsChanged;
    public System.Action onTechChanged;

    // ENEMY KILLS DICTIONARY
    public Dictionary<string,int> killDict = new Dictionary<string,int>();

    // ITEMS IN INVENTORY
    public List<Item> items = new List<Item>();

    // ECONOMY
    public int totalScraps = 0;
    public int totalTechUnits = 0;

    public int kills = 0;
    public int water = 0;
    public int scraps = 0;
    public int techUnits = 0;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;

        killDict.Clear();
        killDict.Add("Shell", 0);
        killDict.Add("Germinite", 0);
        killDict.Add("Gland", 0);
    }

    public void AddItem(Item item)
    {
        items.Add(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void ChangeScraps(int amount)
    {
        scraps += amount;
        onScrapsChanged?.Invoke();
        if (amount > 0) totalScraps += amount;
    }

    public void ChangeTechUnits(int amount)
    {
        techUnits += amount;
        onTechChanged?.Invoke();
        if (amount > 0) totalTechUnits += amount;
    }
}

public enum EquipmentSlot {
    head,
    chest,
    legs,
    weapon
}