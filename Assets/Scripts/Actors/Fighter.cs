using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Movable
{
    // WEAPON
    public Weapon weapon { get; protected set; }
    protected Vector2 firePivot;
    private float firePivotYmod = 0.66f;
    private float firePivotDist = 0.66f;

    // PARAMETERS
    protected Vector3 aimTarget;

    // AWAKE
    protected override void Awake()
    {
        base.Awake();

        // GET WEAPON
        weapon = GetComponentInChildren<Weapon>();
    }

    // UPDATE
    protected override void Update()
    {
    }

    protected void UpdateFirePivot()
    {
        // FIRE PIVOT PLACEMENT
        firePivot = data.facingDir * firePivotDist;
        firePivot.y *= firePivotYmod;
    }

    // FIRE WEAPON
    public void FireWeapon()
    {
        UpdateFirePivot();

        Vector2 muzzlePos = Random.insideUnitCircle * 0.15f;
        Vector3 firePos = new Vector3(transform.position.x + firePivot.x + muzzlePos.x, transform.position.y + firePivot.y + muzzlePos.y, 0f);

        weapon.Fire(firePos, aimTarget);
    }    
}
