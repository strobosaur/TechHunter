using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // STATIC INSTANCE
    public static GameManager instance;

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
    }
}
