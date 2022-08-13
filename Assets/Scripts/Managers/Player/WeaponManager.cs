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
    private ObjectPool<Bullet> glandBulletPool;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Bullet glandBulletPrefab;
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
        
        // CREATE BULLET POOL
        glandBulletPool = new ObjectPool<Bullet>(() => { 
            return Instantiate(glandBulletPrefab);
        }, glandBullet => {
            glandBullet.gameObject.SetActive(true);
        }, glandBullet => {
            glandBullet.gameObject.SetActive(false);
        }, glandBullet => {
            Destroy(glandBullet.gameObject);
        }, false, 100, 1000);
    }

    // SPAWN BULLET
    public Bullet SpawnBullet(bool gland = false)
    {
        if (gland)
            return usePool ? glandBulletPool.Get() : Instantiate(bulletPrefab);
        else
            return usePool ? bulletPool.Get() : Instantiate(bulletPrefab);
    }
}
