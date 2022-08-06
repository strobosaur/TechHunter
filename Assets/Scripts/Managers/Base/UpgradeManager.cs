using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public int[] upgradeCounts = new int[System.Enum.GetNames(typeof(UpgradeType)).Length];

    public void UpgradeWeapon(WeaponStatsObject stats, int level)
    {
        int bonusCounter = 3;
        do {
            
        } while (bonusCounter > 0);

    }

    void WpnUpgFrate(WeaponStatsObject stats, float amount = 0.25f) => stats.frate.AddModifier(-(stats.frate.GetValue() * amount));
    void WpnUpgBrate(WeaponStatsObject stats, float amount = 0.25f) => stats.brate.AddModifier(-(stats.brate.GetValue() * amount));
    void WpnUpgDamage(WeaponStatsObject stats, float amount = 1) => stats.dmg.AddModifier(amount);
    void WpnUpgRange(WeaponStatsObject stats, float amount = 2.5f) => stats.range.AddModifier(amount);
    void WpnUpgShots(WeaponStatsObject stats, int amount = 1) => stats.shots.AddModifier(amount);
    void WpnUpgBurst(WeaponStatsObject stats, int amount = 1) => stats.burst.AddModifier(amount);
    void WpnUpgKnockback(WeaponStatsObject stats, float amount = 0.5f) => stats.knockback.AddModifier(amount);
}

public enum UpgradeType {
    weapon,
    armor,
    helmet,
    boots,
    body
}