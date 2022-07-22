using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class WeaponManager : MonoBehaviour
{
    // MAKE SINGLETON
    public static WeaponManager instance;

    // BULLET POOL
    private ObjectPool<Bullet> bulletPool;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private bool usePool;

    // AWAKE
    void Awake()
    {
        instance = this;
    }

    // START
    void Start()
    {
        // CREATE BULLET POOL
        bulletPool = new ObjectPool<Bullet>(() => { 
            return Instantiate(bulletPrefab);
        }, bullet => {
            bullet.gameObject.SetActive(true);
        }, bullet => {
            bullet.gameObject.SetActive(false);
        }, bullet => {
            Destroy(bullet.gameObject);
        }, false, 100, 1000);
    }

    // SPAWN BULLET
    public Bullet SpawnBullet()
    {
        return usePool ? bulletPool.Get() : Instantiate(bulletPrefab);
    }
}
