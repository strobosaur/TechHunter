using System.Collections.Generic;
using UnityEngine;

public class PropGenerator : MonoBehaviour
{
    public static PropGenerator instance;

    public GameObject propContainer;

    public List<Prop> propList = new List<Prop>();
    public List<PropData> rngList;
    public float totalWeight;
    public float propChance = 0.025f;

    public GameObject propPrefab;

    void Awake()
    {
        instance = this;

        foreach (var prop in rngList)
        {
            totalWeight += prop.rngWeight;
        }

        propContainer = GameObject.Find("PropContainer");
    }

    public void GenerateProps(IEnumerable<Vector2Int> floorPositions)
    {
        ClearAllProps();

        Vector2 propPosition;

        foreach (var position in floorPositions)
        {
            if (Random.value < propChance)
            {
                propPosition = Random.insideUnitCircle * 0.25f;

                propPosition += (Vector2)position;

                var propData = GetRandomPropData();
                var ob = Instantiate(propData.propPrefab, propContainer.transform);
                var prop = ob.GetComponent<Prop>();

                prop.propName = propData.propName;
                prop.FlipSprite();

                prop.transform.position = propPosition;

                propList.Add(prop);
            }
        }
    }

    private PropData GetRandomPropData()
    {
        float rng = Random.Range(0f,totalWeight);
        float counter = 0;
        for (int i = 0; i < rngList.Count; i++)
        {
            if ((rng > counter) && (rng < counter + rngList[i].rngWeight))
            {
                return rngList[i];
            } else {
                counter += rngList[i].rngWeight;
            }
        }

        return rngList[0];
    }

    public void ClearAllProps()
    {
        foreach (var prop in propList)
        {
            Destroy(prop.gameObject);
        }

        propList.Clear();
    }
}