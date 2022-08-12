using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPylon : Interactable
{
    public SpawnPoint spawnPoint;
    public GameObject pylonProp;
    public GameObject pylonText;
    public bool isPoweredUp = false;

    void OnEnable()
    {
        objectName = "Power Up Pylon";
        spawnPoint = GetComponentInParent<SpawnPoint>();
    }

    public override void Interact()
    {
        if ((!isPoweredUp) && (!CurrentLevelManager.instance.waveStarted))
        {
            objectName = "";
            isPoweredUp = true;
            isInteractable = false;
            pylonProp.GetComponent<PowerPylonProp>().PowerUpPylon();
            CurrentLevelManager.instance.StartWave(spawnPoint.gameObject);
            CurrentLevelManager.instance.onWaveCleared += GrantDrops;
        }
    }

    private void GrantDrops()
    {
        int scraps = Random.Range(10,51);
        int tech = Random.Range(0,1) + Mathf.RoundToInt(Random.Range(0f,0.55f));
        Inventory.instance.ChangeScraps(scraps);
        Inventory.instance.ChangeTechUnits(tech);
        CurrentLevelManager.instance.onWaveCleared -= GrantDrops;
    }
}
