using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombat
{
    public void Attack(IWeapon weapon, Vector2 origin, Vector2 target);
}
