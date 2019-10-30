using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    // Array of coordinates virtual agent travels through
    public Transform[] waypoints;

    Animator animator;

    int m_CurrentWayPointIndex;
    float waitTime;     // How long virtual agent waits at a waypoint

    void Start()
    {
        animator = GetComponent<Animator>();

        navMeshAgent.SetDestination(waypoints[0].position);
        animator.SetBool("IsWalking", true);
        waitTime = Random.Range(4.0f, 8.0f);
    }

    void Update()
    {
        // After reaching a waypoint wait for waitTime, then move to next waypoint 
        // If at last waypoint then virtual agent goes to the first waypoint
        if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            animator.SetBool("IsWalking", false);
            waitTime -= Time.deltaTime;

            if (waitTime <= 0)
            {
                animator.SetBool("IsWalking", true);
                m_CurrentWayPointIndex = (m_CurrentWayPointIndex + 1) % waypoints.Length;

                navMeshAgent.SetDestination(waypoints[m_CurrentWayPointIndex].position);
                waitTime = Random.Range(4.0f, 8.0f);
            }
        }
    }
}
