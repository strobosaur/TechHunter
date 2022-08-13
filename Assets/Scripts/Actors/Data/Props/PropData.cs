using UnityEngine;

[CreateAssetMenu(fileName = "newPropData", menuName="Data/Prop Data/New Prop")]
public class PropData : ScriptableObject
{
    public string propName;
    public GameObject propPrefab;
    public float rngWeight;
}
