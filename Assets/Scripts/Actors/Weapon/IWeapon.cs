using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public void WeaponAttack(Vector3 origin, Transform target, WeaponStatsObject wpnParams);
    // public void SetWeaponParams(WeaponParams stats);
    // public WeaponParams GetWeaponParams();
}
