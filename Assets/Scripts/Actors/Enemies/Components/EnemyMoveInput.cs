using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyMoveInput : MonoBehaviour, IMoveInput
{
    public Transform target;
    public Enemy enemy;

    public float nextWaypointDistance = 3f;

    private Path path;
    private int currentWaypoint = 0;
    private bool endOfPath = false;

    private Seeker seeker;

    // START
    void Start()
    {
        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath", 0f, 2 + Random.Range(0f,0.5f));
        seeker.StartPath(transform.position, target.position, OnPathComplete);
    }

    public Vector2 GetMoveInput()
    {
        if ((path == null) || (target == null))
            return Vector2.zero;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            endOfPath = true;
            return Vector2.zero;
        } else {
            endOfPath = false;
        }

        Vector2 direction = ((Vector2)(path.vectorPath[currentWaypoint] - transform.position)).normalized;

        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        
        return direction;
    }

    // UPDATE PATH
    private void UpdatePath()
    {
        if ((seeker.IsDone()) && (target != null))
            seeker.StartPath(transform.position, target.position, OnPathComplete);
    }

    // ON PATH COMPLETE
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
