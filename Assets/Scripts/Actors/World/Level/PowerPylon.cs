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
            // PLAY SOUND EFFECT
            AudioManager.instance.Play("menu_blip");

            objectName = "";
            isPoweredUp = true;
            isInteractable = false;
            CurrentLevelManager.instance.StartWave(spawnPoint.gameObject);
            CurrentLevelManager.instance.onWaveCleared += GrantDrops;
        }
    }

    private void GrantDrops()
    {
        // PLAY SOUND EFFECT
        AudioManager.instance.Play("menu_choice");

        pylonProp.GetComponent<PowerPylonProp>().PowerUpPylon();

        int scraps = Random.Range(10,51);
        int tech = Random.Range(0,1) + Mathf.RoundToInt(Random.Range(0f,0.55f));
        Inventory.instance.ChangeScraps(scraps);
        Inventory.instance.ChangeTechUnits(tech);
        CurrentLevelManager.instance.onWaveCleared -= GrantDrops;
    }
}
