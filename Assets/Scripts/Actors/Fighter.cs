using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Movable
{
    // WEAPON
    protected Vector2 firePivot;
    private float firePivotYmod = 0.66f;
    private float firePivotDist = 0.66f;

    // COMPONENTS
    public HitFlash hitflash;
    public IWeapon weapon;
    public StatsObject stats;
    public WeaponParams wpnStats;

    // AWAKE
    protected override void Awake()
    {
        base.Awake();

        // GET WEAPON
        weapon = GetComponent<IWeapon>();

        // GET HITFLASH
        hitflash = GetComponent<HitFlash>();
    }

    // UPDATE FIRE PIVOT
    protected void UpdateFirePivot(Vector2 dir)
    {
        // FIRE PIVOT PLACEMENT
        firePivot = dir * firePivotDist;
        firePivot.y *= firePivotYmod;
    }

    // FIRE WEAPON
    public void FireWeapon(IWeapon weapon, Transform target, WeaponStatsObject wpnStats)
    {
        UpdateFirePivot((target.position - transform.position).normalized);

        Vector2 muzzlePos = Random.insideUnitCircle * 0.15f;
        Vector3 firePos = new Vector3(transform.position.x + firePivot.x + muzzlePos.x, transform.position.y + firePivot.y + muzzlePos.y, 0f);

        weapon.WeaponAttack(firePos, target, wpnStats);
    }

    // STATS INIT
    public void StatsInit(EntityStats stats, WeaponParams wpnStats)
    {
        this.stats = new StatsObject();

        this.stats.HPmax.SetValue(stats.HPmax.GetValue());
        this.stats.HPcur = this.stats.HPmax.GetValue();

        this.stats.armor.SetValue(stats.armor.GetValue());

        this.stats.staminaMax.SetValue(stats.staminaMax.GetValue());
        this.stats.staminaCur = this.stats.staminaMax.GetValue();

        this.stats.moveSpd.SetValue(stats.moveSpd.GetValue());
        this.stats.moveBoostSpd.SetValue(stats.moveBoostSpd.GetValue());
        this.stats.moveBoostTime.SetValue(stats.moveBoostTime.GetValue());
        this.stats.moveBoostCD.SetValue(stats.moveBoostCD.GetValue());

        this.stats.invincibilityTime.SetValue(stats.invincibilityTime.GetValue());

        this.stats.wpnStats = new WeaponStatsObject();
        this.stats.wpnStats.InitStats(wpnStats);
    }
}
