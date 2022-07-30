using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private float baseValue;
    private List<float> modifiers = new List<float>();

    public float GetValue()
    {
        float outValue = baseValue;
        modifiers.ForEach(x => outValue += x);
        return outValue;
    }

    public void AddModifier(float modifier)
    {
        if (modifier != Mathf.Epsilon)
            modifiers.Add(modifier);
    }

    public void RemoveModifier(float modifier)
    {
        if (modifier != Mathf.Epsilon)
            modifiers.Remove(modifier);
    }
}
