using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void WeaponAttack(Vector3 origin, Vector3 target);
    void UpdateWeapon(Vector2 dir);
}
