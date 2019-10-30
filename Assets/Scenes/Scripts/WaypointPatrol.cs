using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    // Virtual agent
    public NavMeshAgent navMeshAgent;

    // Array of coordinates virtual agent travels through
    public Transform[] waypoints;

    int m_CurrentWayPointIndex;
    Vector3 startingPoint;

    void Start()
    {

        // Start at first waypoint
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    void Update()
    {
        // After reaching waypoint go to next one: if at last one go to waypoint index 0
        if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWayPointIndex = (m_CurrentWayPointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWayPointIndex].position);
        }
    }
}
