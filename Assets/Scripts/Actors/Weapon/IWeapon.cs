using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public void WeaponAttack(Vector3 origin, Transform target);
    public void SetWeaponParams(WeaponParams stats);
    public WeaponParams GetWeaponParams();
}
