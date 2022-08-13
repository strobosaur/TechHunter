using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 target;
    public float moveDelta;
    public Fighter shooter;
    public int targetLayer;
    public DoDamage damage;
    private bool isProjectile = true;

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
        MakeTrail(target);
        transform.position = Vector3.MoveTowards(transform.position, target, moveDelta * Time.deltaTime); 
    }

    // CHECK IF TARGET REACHED
    void CheckForTarget()
    {
        if (Vector2.Distance(transform.position, target) < 0.01f) {
            DestroyBullet();
        }
    }

    // MAKE TRAIL
    void MakeTrail(Vector3 trailTarget)
    {
        Vector3 tempPos;
        int dist = Mathf.RoundToInt(moveDelta);
        for (int i = 0; i < dist; i++)
        {
            tempPos = Vector3.MoveTowards(transform.position,trailTarget, (1f / Globals.G_CELLSIZE) * i);
            if (Vector3.Distance(tempPos,trailTarget) > 0.01f) {
                EffectsManager.dotPS1.Emit(tempPos,Vector3.zero,0.0625f,0.35f,new Color(1f,1f,1f,0.5f));
            }
        }
    }

    // DESTROY BULLET
    void DestroyBullet()
    {
        // MAKE BLAST
        EffectsManager.boomPS.Emit(transform.position,Vector3.zero, 0.75f * Random.Range(0.9f,1.1f), 0.0625f, Color.white);

        // MAKE DUST
        Instantiate(EffectsManager.instance.SpawnDust(transform.position));

        // DESTROY GAME OBJECT
        Destroy(gameObject);
    }

    // CHECK FOR HIT
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        //Debug.Log("Bullet hit " + collision);
        if ((shooter.tag != collision.tag) && (collision.GetComponent<Collider2D>().GetComponent<Bullet>() == null))
        {
            MakeTrail(collision.transform.position);
            DestroyBullet();

            if (collision.GetComponent<IDamageable>() != null)
                collision.GetComponent<IDamageable>().ReceiveDamage(damage, (collision.transform.position - transform.position).normalized);
        }        
    }
}
