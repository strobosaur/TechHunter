using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour, IDamageable
{
    public Enemy owner;

    public void Awake()
    {
        owner = GetComponentInParent<Enemy>();
    }

    public void ReceiveDamage(DoDamage damage, Vector2 origin)
    {
        owner.ReceiveDamage(damage, origin);
    }
}
