using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 target;
    public float moveDelta;

    // AWAKE
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // UPDATE
    void Update()
    {
        CheckForTarget();
    }

    // FIXED UPDATE
    void FixedUpdate()
    {
        MakeTrail();
        transform.position = Vector3.MoveTowards(transform.position, target, moveDelta * Time.deltaTime); 
    }

    // CHECK IF TARGET REACHED
    void CheckForTarget()
    {
        if (Vector2.Distance(transform.position, target) < 0.01f) {

            // MAKE BLAST
            EffectsManager.boomPS.Emit(transform.position,Vector3.zero, 0.75f * Random.Range(0.9f,1.1f), 0.0625f, Color.white);

            // MAKE DUST
            Instantiate(EffectsManager.instance.SpawnDust(transform.position));

            // DESTROY GAME OBJECT
            Destroy(gameObject);
        }
    }

    // MAKE TRAIL
    void MakeTrail()
    {
        Vector3 tempPos;
        int dist = Mathf.RoundToInt(moveDelta);
        for (int i = 0; i < dist; i++)
        {
            tempPos = Vector3.MoveTowards(transform.position,target, (1f / Globals.G_CELLSIZE) * i);
            if (Vector3.Distance(tempPos,target) > 0.01f) {
                EffectsManager.dotPS1.Emit(tempPos,Vector3.zero,0.0625f,0.35f,new Color(1f,1f,1f,0.5f));
            }
        }
    }
}
