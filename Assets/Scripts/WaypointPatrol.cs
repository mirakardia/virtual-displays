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

    Animator animator;

    int m_CurrentWayPointIndex;
    Vector3 startingPoint;
    float waitTime;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Start at first waypoint
        navMeshAgent.SetDestination(waypoints[0].position);
        animator.SetBool("IsWalking", true);
        waitTime = Random.Range(3.0f, 7.0f);
    }

    void Update()
    {
        // After reaching waypoint go to next one: if at last one go to waypoint index 0
        // NOTE: Make sure virtual agent can go from last index to first or change the function

        if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            animator.SetBool("IsWalking", false);

            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                animator.SetBool("IsWalking", true);
                m_CurrentWayPointIndex = (m_CurrentWayPointIndex + 1) % waypoints.Length;
                navMeshAgent.SetDestination(waypoints[m_CurrentWayPointIndex].position);
                waitTime = Random.Range(3.0f, 7.0f);
            }
        }
    }
}
