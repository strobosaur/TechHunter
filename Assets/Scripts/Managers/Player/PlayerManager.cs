using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public List<GameObject> playerList = new List<GameObject>();

    public EntityStats statsBlueprint;
    public WeaponParams wpnStatsBlueprint;
    public StatsObject playerStats;

    public System.Action onGameOver;
    public System.Action onHPchanged;

    void Awake()
    {
        instance = this;
    }

    // STATS INIT
    public void StatsInit(EntityStats stats, WeaponParams wpnStats)
    {
        this.playerStats = new StatsObject();

        this.playerStats.HPmax.SetValue(stats.HPmax.GetValue());
        this.playerStats.HPcur = this.playerStats.HPmax.GetValue();

        this.playerStats.armor.SetValue(stats.armor.GetValue());

        this.playerStats.staminaMax.SetValue(stats.staminaMax.GetValue());
        this.playerStats.staminaCur = this.playerStats.staminaMax.GetValue();

        this.playerStats.moveSpd.SetValue(stats.moveSpd.GetValue());
        this.playerStats.moveBoostSpd.SetValue(stats.moveBoostSpd.GetValue());
        this.playerStats.moveBoostTime.SetValue(stats.moveBoostTime.GetValue());
        this.playerStats.moveBoostCD.SetValue(stats.moveBoostCD.GetValue());

        this.playerStats.invincibilityTime.SetValue(stats.invincibilityTime.GetValue());

        this.playerStats.wpnStats = new WeaponStatsObject();
        this.playerStats.wpnStats.InitStats(wpnStats);
    }

    // CHANGE HP VALUE
    public void ChangeHP(float amount)
    {
        if (amount > 0) playerStats.HPcur = Globals.Approach(playerStats.HPcur, playerStats.HPmax.GetValue(), amount);
        else playerStats.HPcur = Globals.Approach(playerStats.HPcur, 0, amount);
        onHPchanged?.Invoke();
    }

    // FIND ALL PLAYERS
    public void FindPlayers()
    {
        playerList.Clear();
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in players)
        {
            playerList.Add(item);
        }
    }

    // RESET PLAYER SESSION
    public void ResetGameSession()
    {
        StatsInit(statsBlueprint, wpnStatsBlueprint);
        Inventory.instance.ResetInventory();
        LevelManager.instance.ResetGameSession();
        GameManager.instance.upgManager.ResetGameSession();
    }
}
