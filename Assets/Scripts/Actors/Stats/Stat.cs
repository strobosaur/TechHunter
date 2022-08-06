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

    public void SetValue(float value)
    {
        baseValue = value;
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

[System.Serializable]
public class StatInt
{
    [SerializeField] private int baseValue;
    private List<int> modifiers = new List<int>();

    public int GetValue()
    {
        int outValue = baseValue;
        modifiers.ForEach(x => outValue += x);
        return outValue;
    }

    public void SetValue(int value)
    {
        baseValue = value;
    }

    public void AddModifier(int modifier)
    {
        if (modifier != 0)
            modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
            modifiers.Remove(modifier);
    }
}
