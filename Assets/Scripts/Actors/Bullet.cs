using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 target;
    public float moveDelta;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckForTarget();
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, moveDelta * Time.deltaTime); 
    }

    void CheckForTarget()
    {
        if (Vector2.Distance(transform.position, target) < 0.1f) {
            GameManager.boomPS.Emit(transform.position,Vector3.zero, 0.75f * Random.Range(0.9f,1.1f), 0.0625f, Color.white);
            Destroy(gameObject);
        }
    }
}
