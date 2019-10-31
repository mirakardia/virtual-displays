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
    bool playerInSight;
    float waitTime;     // How long virtual agent waits at a waypoint
    float arrivalTime;  // When VA arrived to a waypoint

    void Start()
    {
        animator = GetComponent<Animator>();

        navMeshAgent.SetDestination(waypoints[0].position);
        animator.SetBool("IsWalking", true);
        waitTime = Random.Range(4.0f, 8.0f);
    }

    void Update()
    {
        if (playerInSight)
        {
            Debug.Log("pelaaja spotattu'd");
        }

        else if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            if (animator.GetBool("IsWalking") == true)
            {
                arriveToWaypoint();
            }
            
            else if (Time.time > arrivalTime + waitTime)
            {
                moveToNextWaypoint();
            }
        }
    }

    private void playerEnter()
    {
        playerInSight = true;
    }

    private void playerExit()
    {
        playerInSight = false;
        moveToNextWaypoint();
    }

    private void arriveToWaypoint()
    {
        animator.SetBool("IsWalking", false);
        waitTime = Random.Range(4.0f, 8.0f);
        arrivalTime = Time.time;
        m_CurrentWayPointIndex = (m_CurrentWayPointIndex + 1) % waypoints.Length;
    }

    private void moveToNextWaypoint()
    {
        animator.SetBool("IsWalking", true);
        navMeshAgent.SetDestination(waypoints[m_CurrentWayPointIndex].position);
    }
}
