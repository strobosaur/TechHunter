using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Movable
{
    // WEAPON
    protected Weapon weapon;
    protected Vector2 firePivot;
    private float firePivotYmod = 0.66f;
    private float firePivotDist = 0.66f;

    // PARAMETERS
    protected Transform aimTarget;

    // AWAKE
    protected override void Awake()
    {
        base.Awake();
    }

    // UPDATE
    protected override void Update()
    {
        GetFacingDir(aimTarget.position);
        UpdateFirePivot();
        UpdateAnimator();
    }

    // UPDATE ANIMATOR
    protected override void UpdateAnimator()
    {
        if (anim != null) 
        {
            // FACE MOVEMENT DIRECTION
            if (moveDelta.magnitude > 0.1f) {
                anim.SetFloat("velX", moveDelta.x);
                anim.SetFloat("velY", moveDelta.y);
                if (weapon != null) weapon.UpdateWeapon(moveDelta);

                // UPDATE ANIMATOR PARAMETERS
                anim.SetFloat("magnitude", moveDelta.magnitude);
                anim.speed = moveDelta.magnitude * animSpd;
            } else {
                // UPDATE ANIMATOR PARAMETERS
                anim.SetFloat("magnitude", 0f);
                anim.speed = 0f;
            }

            // IF HAS TARGET, FACE TARGET POSITION
            if (facingDir.magnitude > 0.2)
            {
                anim.SetFloat("velX", facingDir.x);
                anim.SetFloat("velY", facingDir.y);
                if (weapon != null) weapon.UpdateWeapon(facingDir);
            }
        }
    }

    protected void UpdateFirePivot()
    {
        // FIRE PIVOT PLACEMENT
        firePivot = facingDir * firePivotDist;
        firePivot.y *= firePivotYmod;
    }

    // FIRE WEAPON
    protected void FireWeapon()
    {
        Vector2 muzzlePos = Random.insideUnitCircle * 0.15f;
        Vector3 firePos = new Vector3(transform.position.x + firePivot.x + muzzlePos.x, transform.position.y + firePivot.y + muzzlePos.y, 0f);

        weapon.Fire(firePos, aimTarget.position);
    }
}
