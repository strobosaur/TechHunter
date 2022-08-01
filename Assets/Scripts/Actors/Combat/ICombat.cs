using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombat
{
    public void Attack(IWeapon weapon, Vector2 origin, Transform target);
    public void UpdateWeapon(Vector2 dir);
}
