using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour, ICombat
{
    public Enemy enemy;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    public void Attack(IWeapon weapon, Vector2 origin, Vector2 target)
    {
        enemy.FireWeapon(target);
    }
}
