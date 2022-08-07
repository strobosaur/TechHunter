using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public int[] upgradeCounts = new int[System.Enum.GetNames(typeof(UpgradeType)).Length];

    public List<UpgItem> wpnUpgList = new List<UpgItem>();
    public List<UpgItem> armorUpgList;

    public System.Action onStatsChanged;

    // AWAKE
    void Awake()
    {
        // WEAPON UPGRADES
        wpnUpgList.Add(new UpgItem("WpnUpgFrate", 4));
        wpnUpgList.Add(new UpgItem("WpnUpgBrate", 3));
        wpnUpgList.Add(new UpgItem("WpnUpgDamage", 8));
        wpnUpgList.Add(new UpgItem("WpnUpgRange", 2));
        wpnUpgList.Add(new UpgItem("WpnUpgSpr", 3));
        wpnUpgList.Add(new UpgItem("WpnUpgDmgSpr", 1));
        wpnUpgList.Add(new UpgItem("WpnUpgShots", 1));
        wpnUpgList.Add(new UpgItem("WpnUpgBurst", 4));
        wpnUpgList.Add(new UpgItem("WpnUpgKnockback", 2));
    }

    // START
    void Start()
    {
        
    }

    // UPGRADE WEAPON
    public void UpgradeWeapon(WeaponStatsObject stats, int level)
    {
        int bonusCounter = 3;
        do {
            switch (GetWeightedRandomUpg(wpnUpgList).upgName)
            {
                case "WpnUpgFrate":
                WpnUpgFrate(stats);
                break;

                case "WpnUpgBrate":
                WpnUpgBrate(stats);
                break;

                case "WpnUpgDamage":
                WpnUpgDamage(stats);
                break;

                case "WpnUpgRange":
                WpnUpgRange(stats);
                break;

                case "WpnUpgSpr":
                WpnUpgSpr(stats);
                break;

                case "WpnUpgDmgSpr":
                WpnUpgDmgSpr(stats);
                break;

                case "WpnUpgShots":
                WpnUpgShots(stats);
                break;

                case "WpnUpgBurst":
                WpnUpgBurst(stats);
                break;

                case "WpnUpgKnockback":
                WpnUpgKnockback(stats);
                break;
            }
            
            bonusCounter--;
        } while (bonusCounter > 0);

        // FIRE EVENTS ON STAT CHANGED
        onStatsChanged?.Invoke();
    }

    // GET WEIGHTED RANDOM
    UpgItem GetWeightedRandomUpg(List<UpgItem> upgList)
    {
        float totalWeight = 0;
        float rng;
        float counter = 0;
        int i = 0;

        foreach (var upg in upgList)
        {
            totalWeight += upg.weight;
        }

        rng = Random.Range(0f, totalWeight);
        
        for (i = 0; i < upgList.Count; i++)
        {
            if ((rng > counter) && (rng < counter + upgList[i].weight))
            {
                return upgList[i];
            } else {
                counter += upgList[i].weight;
            }
        }

        return upgList[i];
    }

    void WpnUpgFrate(WeaponStatsObject stats, float amount = 0.25f) => stats.frate.AddModifier(-(stats.frate.GetValue() * amount));
    void WpnUpgBrate(WeaponStatsObject stats, float amount = 0.25f) => stats.brate.AddModifier(-(stats.brate.GetValue() * amount));
    void WpnUpgDamage(WeaponStatsObject stats, float amount = 1) => stats.dmg.AddModifier(amount);
    void WpnUpgRange(WeaponStatsObject stats, float amount = 2.5f) => stats.range.AddModifier(amount);
    void WpnUpgSpr(WeaponStatsObject stats, float amount = 0.25f) => stats.spr.AddModifier(-(stats.spr.GetValue() * amount));
    void WpnUpgDmgSpr(WeaponStatsObject stats, float amount = 0.25f) => stats.dmgSpr.AddModifier(-(stats.dmgSpr.GetValue() * amount));
    void WpnUpgShots(WeaponStatsObject stats, int amount = 1) => stats.shots.AddModifier(amount);
    void WpnUpgBurst(WeaponStatsObject stats, int amount = 1) => stats.burst.AddModifier(amount);
    void WpnUpgKnockback(WeaponStatsObject stats, float amount = 0.5f) => stats.knockback.AddModifier(amount);

    void StatsUpgHP(StatsObject stats, int amount = 5) => stats.HPmax.AddModifier(amount);
    void StatsUpgStamina(StatsObject stats, float amount = 10) => stats.staminaMax.AddModifier(amount);
    void StatsUpgMoveSpd(StatsObject stats, float amount = 0.25f) => stats.moveSpd.AddModifier(amount);
    void StatsUpgMoveBoostSpd(StatsObject stats, float amount = 0.5f) => stats.moveBoostSpd.AddModifier(amount);
    void StatsUpgMoveBoostTime(StatsObject stats, float amount = 0.5f) => stats.moveBoostTime.AddModifier(amount);
}

public class UpgItem
{
    public UpgItem(string upgName, float weight)
    {
        this.upgName = upgName;
        this.weight = weight;
    }

    public string upgName;
    public float weight;
    public System.Action upgAction;
}

public enum UpgradeType {
    weapon,
    armor,
    helmet,
    boots,
    body
}