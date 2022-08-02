using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager instance;

    // PARTICLE SYSTEMS
    public static ParticleSystem boomPS;
    public static ParticleSystem dotPS1;

    // DUST PS POOL
    private ObjectPool<ParticleSystem> dustPool;
    private ObjectPool<ParticleSystem> bloodPool01;
    private ObjectPool<ParticleSystem> bloodPool02;

    public ParticleSystem dustPSPrefab;
    public ParticleSystem blood01PSPrefab;
    public ParticleSystem blood02PSPrefab;

    void Awake()
    {
        instance = this;
        boomPS = GameObject.Find("BoomPS").GetComponent<ParticleSystem>();
        dotPS1 = GameObject.Find("DotPS1").GetComponent<ParticleSystem>();
    }

    void Start()
    {
        // CREATE DUST POOL
        dustPool = new ObjectPool<ParticleSystem>(() => { 
            return Instantiate(dustPSPrefab);
        }, dustPS => {
            dustPS.gameObject.SetActive(true);
        }, dustPS => {
            dustPS.gameObject.SetActive(false);
        }, dustPS => {
            Destroy(dustPS.gameObject);
        }, false, 100, 1000);
        
        // CREATE BLOOD POOL 1
        bloodPool01 = new ObjectPool<ParticleSystem>(() => { 
            return Instantiate(blood01PSPrefab);
        }, bloodPS01 => {
            bloodPS01.gameObject.SetActive(true);
        }, bloodPS01 => {
            bloodPS01.gameObject.SetActive(false);
        }, bloodPS01 => {
            Destroy(bloodPS01.gameObject);
        }, false, 100, 1000);
        
        // CREATE BLOOD POOL 2
        bloodPool02 = new ObjectPool<ParticleSystem>(() => { 
            return Instantiate(blood02PSPrefab);
        }, bloodPS02 => {
            bloodPS02.gameObject.SetActive(true);
        }, bloodPS02 => {
            bloodPS02.gameObject.SetActive(false);
        }, bloodPS02 => {
            Destroy(bloodPS02.gameObject);
        }, false, 100, 1000);
    }

    // SPAWN DUST
    public ParticleSystem SpawnDust(Vector2 pos)
    {
        var dustPS = dustPool.Get();
        dustPS.transform.position = pos;
        return dustPS;
    }

    // SPAWN BLOOD 1
    public ParticleSystem SpawnBlood01(Vector2 pos)
    {
        var bloodPS = bloodPool01.Get();
        bloodPS.transform.position = pos;
        return bloodPS;
    }

    // SPAWN BLOOD 2
    public ParticleSystem SpawnBlood02(Vector2 pos)
    {
        var bloodPS = bloodPool02.Get();
        bloodPS.transform.position = pos;
        return bloodPS;
    }

    // public void PlayDustPS(Vector3 position, int count = 1, float radius = 0f, float time = 1f, float spd = 0f, float size = 0.5f)
    // {
    //     for (int i = 0; i < count; i++)
    //     {
    //         Vector2 rnd = Random.insideUnitCircle * radius;
    //         rnd.x += position.x;
    //         rnd.y += position.y;
    //         puffPS.Emit(rnd,Vector3.up * Random.Range(0, spd), size, time * Random.Range(0.5f, 1.0f), Color.white);
    //     }
    // }
}
