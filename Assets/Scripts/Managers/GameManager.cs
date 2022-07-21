using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // STATIC INSTANCE
    public static GameManager instance;

    // PARTICLE SYSTEMS
    public static ParticleSystem boomPS;

    void Awake()
    {
        // CREATE SINGLETON
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        boomPS = GameObject.Find("BoomPS").GetComponent<ParticleSystem>();
    }
}
