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

    public ParticleSystem dustPSPrefab;

    void Awake()
    {
        instance = this;
        boomPS = GameObject.Find("BoomPS").GetComponent<ParticleSystem>();
        dotPS1 = GameObject.Find("DotPS1").GetComponent<ParticleSystem>();
    }

    void Start()
    {
        // CREATE BULLET POOL
        dustPool = new ObjectPool<ParticleSystem>(() => { 
            return Instantiate(dustPSPrefab);
        }, dustPS => {
            dustPS.gameObject.SetActive(true);
        }, dustPS => {
            dustPS.gameObject.SetActive(false);
        }, dustPS => {
            Destroy(dustPS.gameObject);
        }, false, 100, 1000);
    }

    // SPAWN BULLET
    public ParticleSystem SpawnDust(Vector2 pos)
    {
        var dustPS = dustPool.Get();
        dustPS.transform.position = pos;
        return dustPS;
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
