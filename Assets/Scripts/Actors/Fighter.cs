using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Movable
{
    // WEAPON
    protected Vector2 firePivot;
    private float firePivotYmod = 0.66f;
    private float firePivotDist = 0.66f;

    // AWAKE
    protected override void Awake()
    {
        base.Awake();

        // GET WEAPON
        weapon = GetComponentInChildren<IWeapon>();
    }

    // UPDATE FIRE PIVOT
    protected void UpdateFirePivot(Vector2 dir)
    {
        // FIRE PIVOT PLACEMENT
        firePivot = dir * firePivotDist;
        firePivot.y *= firePivotYmod;
    }

    // FIRE WEAPON
    public void FireWeapon(Vector2 target)
    {
        UpdateFirePivot(((Vector3)target - transform.position).normalized);

        Vector2 muzzlePos = Random.insideUnitCircle * 0.15f;
        Vector3 firePos = new Vector3(transform.position.x + firePivot.x + muzzlePos.x, transform.position.y + firePivot.y + muzzlePos.y, 0f);

        weapon.WeaponAttack(firePos, target);
    }    
}
