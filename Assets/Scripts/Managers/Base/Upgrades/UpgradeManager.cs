using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // UPGRADE STATS COUNTERS
    public int[] upgradeCounts = new int[System.Enum.GetNames(typeof(UpgradeType)).Length];
    public int[] upgradeCostsTech = new int[System.Enum.GetNames(typeof(UpgradeType)).Length];
    public int[] upgradeCostsScraps = new int[System.Enum.GetNames(typeof(UpgradeType)).Length];

    // UPGRADE ITEM LISTS
    public List<UpgItem> wpnUpgList = new List<UpgItem>();
    public List<UpgItem> armorUpgList = new List<UpgItem>();
    public List<UpgItem> headUpgList = new List<UpgItem>();
    public List<UpgItem> bootsUpgList = new List<UpgItem>();
    public List<UpgItem> bodyUpgList = new List<UpgItem>();

    public float upgradeDeprecator = 0.66f;

    public System.Action onStatsChanged;

    // AWAKE
    void Awake()
    {
        // WEAPON UPGRADES
        wpnUpgList.Add(new UpgItem(UpgStat.wpnFrate, 8));
        //wpnUpgList.Add(new UpgItem(UpgStat.wpnBrate, 3));
        wpnUpgList.Add(new UpgItem(UpgStat.wpnDmg, 30));
        wpnUpgList.Add(new UpgItem(UpgStat.wpnRange, 4));
        wpnUpgList.Add(new UpgItem(UpgStat.wpnDmgSpr, 1));
        wpnUpgList.Add(new UpgItem(UpgStat.wpnShots, 1));
        wpnUpgList.Add(new UpgItem(UpgStat.wpnBurst, 4));

        // ARMOR UPGRADES
        armorUpgList.Add(new UpgItem(UpgStat.armorUpg, 20));
        armorUpgList.Add(new UpgItem(UpgStat.HPupg, 6));
        armorUpgList.Add(new UpgItem(UpgStat.moveSpd, 1));
        armorUpgList.Add(new UpgItem(UpgStat.wpnKnockback, 3));
        armorUpgList.Add(new UpgItem(UpgStat.wpnSpr, 3));
        armorUpgList.Add(new UpgItem(UpgStat.wpnBrate, 3));

        // HEAD UPGRADES
        headUpgList.Add(new UpgItem(UpgStat.armorUpg, 5));
        headUpgList.Add(new UpgItem(UpgStat.HPupg, 2));
        headUpgList.Add(new UpgItem(UpgStat.wpnShots, 1));
        headUpgList.Add(new UpgItem(UpgStat.wpnDmgSpr, 1));
        headUpgList.Add(new UpgItem(UpgStat.wpnSpr, 1));
        headUpgList.Add(new UpgItem(UpgStat.wpnRange, 1));

        // BOOTS UPGRADES
        bootsUpgList.Add(new UpgItem(UpgStat.armorUpg, 2));
        bootsUpgList.Add(new UpgItem(UpgStat.moveSpd, 25));
        bootsUpgList.Add(new UpgItem(UpgStat.HPupg, 2));
        bootsUpgList.Add(new UpgItem(UpgStat.moveBoostSpd, 2));
        bootsUpgList.Add(new UpgItem(UpgStat.moveBoostTime, 2));

        // BODE UPGRADES
        bodyUpgList.Add(new UpgItem(UpgStat.HPupg, 25));
        bodyUpgList.Add(new UpgItem(UpgStat.armorUpg, 10));
        bodyUpgList.Add(new UpgItem(UpgStat.wpnBurst, 2));
        bodyUpgList.Add(new UpgItem(UpgStat.moveSpd, 1));
        bodyUpgList.Add(new UpgItem(UpgStat.wpnKnockback, 2));
    }

    // RESET GAME SESSION
    public void ResetGameSession()
    {
        upgradeCounts = new int[System.Enum.GetNames(typeof(UpgradeType)).Length];
        upgradeCostsTech = new int[System.Enum.GetNames(typeof(UpgradeType)).Length];
        upgradeCostsScraps = new int[System.Enum.GetNames(typeof(UpgradeType)).Length];

        UpdateCosts();
    }

    // UPDATE UPGRADE COSTS
    private void UpdateCosts()
    {
        for (int i = 0; i < (int)UpgradeType.tech; i++)
        {
            upgradeCostsTech[i] = 1 + (Mathf.FloorToInt(upgradeCounts[i] * 1.75f));
            upgradeCostsScraps[i] = 25 + (Mathf.FloorToInt(upgradeCounts[i] * 50f));
        }

        upgradeCostsScraps[(int)UpgradeType.tech] = 500;
        upgradeCostsTech[(int)UpgradeType.tech] = 0;
    }

    // HANDLE UPGRADE
    public bool HandleUpgrade(StatsObject stats, UpgradeType upgType, int level = 1)
    {
        UpdateCosts();

        // CHECK ENOUGH RESOURCES
        if ((Inventory.instance.scraps >= upgradeCostsScraps[(int)upgType])
        && (Inventory.instance.techUnits >= upgradeCostsTech[(int)upgType]))
        {

            // PLAY SOUND EFFECT
            AudioManager.instance.Play("menu_choice");

            // CONSUME RESOURCES
            Inventory.instance.ChangeScraps(-upgradeCostsScraps[(int)upgType]);
            Inventory.instance.ChangeTechUnits(-upgradeCostsTech[(int)upgType]);

            // MAKE CORRECT UPGRADE TYPE
            switch (upgType)
            {
                case UpgradeType.weapon:
                upgradeCounts[(int)upgType]++;
                wpnUpgList = UpgradeStats(stats, wpnUpgList, level);
                break;
                
                case UpgradeType.armor:
                upgradeCounts[(int)upgType]++;
                armorUpgList = UpgradeStats(stats, armorUpgList, level);
                break;
                
                case UpgradeType.head:
                upgradeCounts[(int)upgType]++;
                headUpgList = UpgradeStats(stats, headUpgList, level);
                break;
                
                case UpgradeType.boots:
                upgradeCounts[(int)upgType]++;
                bootsUpgList = UpgradeStats(stats, bootsUpgList, level);
                break;
                
                case UpgradeType.body:
                upgradeCounts[(int)upgType]++;
                bodyUpgList = UpgradeStats(stats, armorUpgList, level);
                break;
                
                case UpgradeType.tech:
                upgradeCounts[(int)upgType]++;
                Inventory.instance.ChangeTechUnits(1);
                break;
            }

            // UPDATE COSTS
            UpdateCosts();

            // FIRE EVENTS ON STAT CHANGED
            onStatsChanged?.Invoke();
            PlayerManager.instance.ChangeHP(PlayerManager.instance.playerStats.HPmax.GetValue());

            return true;
        } else {
            return false;
        }
    }

    // UPGRADE STATS
    public List<UpgItem> UpgradeStats(StatsObject stats, List<UpgItem> upgList, int level = 3, bool deprecate = true)
    {
        int bonusCounter = level;
        do {
            var upgrade = GetWeightedRandomUpg(upgList, deprecate);
            upgList = upgrade.Item2;

            switch (upgrade.Item1.upgStat)
            {
                // WEAPON STATS
                case UpgStat.wpnFrate:
                WpnUpgFrate(stats.wpnStats);
                break;

                case UpgStat.wpnBrate:
                WpnUpgBrate(stats.wpnStats);
                break;

                case UpgStat.wpnDmg:
                WpnUpgDamage(stats.wpnStats);
                break;

                case UpgStat.wpnRange:
                WpnUpgRange(stats.wpnStats);
                break;

                case UpgStat.wpnSpr:
                WpnUpgSpr(stats.wpnStats);
                break;

                case UpgStat.wpnDmgSpr:
                WpnUpgDmgSpr(stats.wpnStats);
                break;

                case UpgStat.wpnShots:
                WpnUpgShots(stats.wpnStats);
                break;

                case UpgStat.wpnBurst:
                WpnUpgBurst(stats.wpnStats);
                break;

                case UpgStat.wpnKnockback:
                WpnUpgKnockback(stats.wpnStats);
                break;

                // BODY STATS
                case UpgStat.HPupg:
                StatsUpgHP(stats);
                break;
                
                case UpgStat.armorUpg:
                StatsUpgArmor(stats);
                break;
                
                case UpgStat.moveSpd:
                StatsUpgMoveSpd(stats);
                break;
                
                case UpgStat.moveBoostSpd:
                StatsUpgMoveBoostSpd(stats);
                break;
                
                case UpgStat.moveBoostTime:
                StatsUpgMoveBoostTime(stats);
                break;
                
                case UpgStat.staminaUpg:
                StatsUpgStamina(stats);
                break;
            }
            
            bonusCounter--;
        } while (bonusCounter > 0);

        // RETURN LIST
        return upgList;
    }

    // GET WEIGHTED RANDOM
    (UpgItem, List<UpgItem>) GetWeightedRandomUpg(List<UpgItem> upgList, bool deprecate = true)
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
                if (deprecate) upgList[i].weight *= upgradeDeprecator;
                return (upgList[i], upgList);
            } else {
                counter += upgList[i].weight;
            }
        }

        if (deprecate) upgList[i].weight *= upgradeDeprecator;
        return (upgList[i], upgList);
    }

    // UPGRADE FUNCTIONS
    void WpnUpgFrate(WeaponStatsObject stats, float amount = 0.125f) => stats.frate.AddModifier(-(stats.frate.GetValue() * amount));
    void WpnUpgBrate(WeaponStatsObject stats, float amount = 0.125f) => stats.brate.AddModifier(-(stats.brate.GetValue() * amount));
    void WpnUpgDamage(WeaponStatsObject stats, float amount = 0.25f) => stats.dmg.AddModifier(amount);
    void WpnUpgRange(WeaponStatsObject stats, float amount = 2f) => stats.range.AddModifier(amount);
    void WpnUpgSpr(WeaponStatsObject stats, float amount = 0.2f) => stats.spr.AddModifier(-(stats.spr.GetValue() * amount));
    void WpnUpgDmgSpr(WeaponStatsObject stats, float amount = 0.2f) => stats.dmgSpr.AddModifier(-(stats.dmgSpr.GetValue() * amount));
    void WpnUpgShots(WeaponStatsObject stats, int amount = 1) => stats.shots.AddModifier(amount);
    void WpnUpgBurst(WeaponStatsObject stats, int amount = 1) => stats.burst.AddModifier(amount);
    void WpnUpgKnockback(WeaponStatsObject stats, float amount = 0.5f) => stats.knockback.AddModifier(amount);

    void StatsUpgArmor(StatsObject stats, int amount = 1) => stats.armor.AddModifier(amount);
    void StatsUpgStamina(StatsObject stats, float amount = 10) => stats.staminaMax.AddModifier(amount);
    void StatsUpgMoveSpd(StatsObject stats, float amount = 0.25f) => stats.moveSpd.AddModifier(amount);
    void StatsUpgMoveBoostSpd(StatsObject stats, float amount = 0.25f) => stats.moveBoostSpd.AddModifier(amount);
    void StatsUpgMoveBoostTime(StatsObject stats, float amount = 0.25f) => stats.moveBoostTime.AddModifier(amount);
    void StatsUpgHP(StatsObject stats, int amount = 5){
        stats.HPmax.AddModifier(amount);
        stats.HPcur = stats.HPmax.GetValue();
    } 
}

// UPGRADE ITEM CLASS
public class UpgItem
{
    public UpgItem(UpgStat upgStat, float weight)
    {
        //this.upgName = upgName;
        this.upgStat = upgStat;
        this.weight = weight;
    }

    public UpgStat upgStat;
    public float weight;
}

// UPGRADE TYPE ENUM
public enum UpgradeType {
    weapon,
    armor,
    head,
    boots,
    body,
    tech
}

// UPGRADE STAT ENUM
public enum UpgStat {
    wpnFrate,
    wpnBrate,
    wpnDmg,
    wpnRange,
    wpnSpr,
    wpnDmgSpr,
    wpnKnockback,
    wpnShots,
    wpnBurst,
    moveSpd,
    moveBoostSpd,
    moveBoostTime,
    HPupg,
    armorUpg,
    invincibilityUpg,
    staminaUpg
}
