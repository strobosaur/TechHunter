using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public void WeaponAttack(Vector3 origin, Vector3 target);
    public void UpdateWeapon(Vector2 dir);
    public void SetWeaponParams(WeaponParams stats);
    public WeaponParams GetWeaponParams();
}
