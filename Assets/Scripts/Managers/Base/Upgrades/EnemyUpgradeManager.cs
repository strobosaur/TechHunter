using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUpgradeManager : MonoBehaviour
{
    List<UpgItem> enemyUpg = new List<UpgItem>();

    // Start is called before the first frame update
    void Awake()
    {
        // WEAPON UPGRADES
        enemyUpg.Add(new UpgItem(UpgStat.wpnFrate, 6));
        enemyUpg.Add(new UpgItem(UpgStat.wpnBrate, 3));
        enemyUpg.Add(new UpgItem(UpgStat.wpnDmg, 20));
        enemyUpg.Add(new UpgItem(UpgStat.wpnRange, 2));
        enemyUpg.Add(new UpgItem(UpgStat.wpnSpr, 3));
        enemyUpg.Add(new UpgItem(UpgStat.wpnDmgSpr, 1));
        enemyUpg.Add(new UpgItem(UpgStat.wpnShots, 1));
        enemyUpg.Add(new UpgItem(UpgStat.wpnBurst, 3));
        enemyUpg.Add(new UpgItem(UpgStat.wpnKnockback, 2));

        // ARMOR UPGRADES
        enemyUpg.Add(new UpgItem(UpgStat.armorUpg, 10));
        enemyUpg.Add(new UpgItem(UpgStat.HPupg, 30));

        // BOOTS UPGRADES
        enemyUpg.Add(new UpgItem(UpgStat.moveBoostSpd, 2));
        enemyUpg.Add(new UpgItem(UpgStat.moveBoostTime, 2));        
    }
    
    public void HandleEnemyUpgrade(StatsObject stats, int upgrades)
    {
        GameManager.instance.upgManager.UpgradeStats(stats, enemyUpg, upgrades, false);
    }
}
