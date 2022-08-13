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

        float diffMod1 = (LevelManager.instance.difficulty / 20f);
        float diffMod2 = 1f + (LevelManager.instance.difficulty / 20f);
        int scraps = Random.Range(10, Mathf.RoundToInt(51f * diffMod2));
        int tech = Random.Range(0, 2) + Mathf.RoundToInt(Random.Range(0f, 0.55f + diffMod1));
        Inventory.instance.ChangeScraps(scraps);
        Inventory.instance.ChangeTechUnits(tech);
        CurrentLevelManager.instance.onWaveCleared -= GrantDrops;
    }
}
