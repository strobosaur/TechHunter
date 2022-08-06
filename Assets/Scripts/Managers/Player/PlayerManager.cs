using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public List<GameObject> playerList;

    public EntityStats statsBlueprint;
    public WeaponParams wpnStatsBlueprint;
    public StatsObject playerStats;

    void Awake()
    {
        instance = this;
        StatsInit(statsBlueprint, wpnStatsBlueprint);

        FindPlayers();
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

    // FIND ALL PLAYERS
    private void FindPlayers()
    {        
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in players)
        {
            playerList.Add(item);
        }
    }
}
