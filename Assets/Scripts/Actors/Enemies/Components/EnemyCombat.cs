using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour, ICombat
{
    public Enemy enemy;

    void OnEnable()
    {
        enemy = GetComponent<Enemy>();
    }

    public void Attack(IWeapon weapon, Vector2 origin, Transform target)
    {
        enemy.FireWeapon(weapon, target);
    }

    public void UpdateWeapon(Vector2 dir)
    {
        
    }
}
