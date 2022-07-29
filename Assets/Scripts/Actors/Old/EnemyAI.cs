using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAI : Movable
{
    public Transform target;

    public float nextWaypointDistance = 3f;

    private Path path;
    private int currentWaypoint = 0;
    private bool endOfPath = false;

    private Seeker seeker;

    // Start is called before the first frame update
    void Start()
    {
        //moveSpd = 2f;
        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        seeker.StartPath(transform.position, target.position, OnPathComplete);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            endOfPath = true;
            return;
        } else {
            endOfPath = false;
        }

        Vector2 direction = ((Vector2)(path.vectorPath[currentWaypoint] - transform.position)).normalized;
        //moveInput = direction;

        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(transform.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
