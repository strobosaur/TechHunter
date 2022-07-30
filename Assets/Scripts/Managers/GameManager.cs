using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // STATIC INSTANCE
    public static GameManager instance;

    // PLAYER MANAGER
    public PlayerManager playerManager;

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

        playerManager = GetComponent<PlayerManager>();
    }
}
