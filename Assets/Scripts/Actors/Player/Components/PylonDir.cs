using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonDir : MonoBehaviour
{
    private Player player;
    private TechPylon techPylon;
    private float playerDist = 1.25f;
    private float baseAlpha = 0.66f;
    private SpriteRenderer sr;
    private Color col;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        player = GetComponentInParent<Player>();
        techPylon = GameObject.Find("TechPylon").GetComponent<TechPylon>();        
    }

    void Update()
    {
        UpdateSpriteAlpha();
    }

    void FixedUpdate()
    {
        FindPylonDir();
    }

    void FindPylonDir()
    {
        Vector2 dir = (techPylon.transform.position - player.transform.position).normalized;
        transform.position = player.transform.position + (Vector3)(dir * playerDist);
    }    

    // SPRITE ALPHA
    void UpdateSpriteAlpha()
    {
        col = sr.color;
        col.a = baseAlpha * (1f - (DisplayManager.instance.textAlpha1 * 0.3f)) * (1f - (DisplayManager.instance.textAlpha2 * 0.15f));
        sr.color = col;
    }
}
