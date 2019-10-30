using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    // Virtual agent
    public NavMeshAgent navMeshAgent;

    // private Animator m_Animator;

    // Array of coordinates virtual agent travels through
    public Transform[] waypoints;

    int m_CurrentWayPointIndex;
    Vector3 startingPoint;

    void Start()
    {
        startingPoint = new Vector3(-53.3f, 2.2f, -0.3f);
        // Start at first waypoint: WIP

        navMeshAgent.Warp(startingPoint);

        // m_rb.GetComponent<Rigidbody>();
        // m_Animator.GetComponent<Animator>();

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
