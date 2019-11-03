using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrolOptimized : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    // Array of coordinates virtual agent travels through
    public Transform[] waypoints;
    public Transform player;

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
            if (animator.GetBool("IsWalking") == true)
            {
                animator.SetBool("IsWalking", false);
                navMeshAgent.isStopped = true;
            }

            if (player && Vector3.Distance(player.position, this.transform.position) >= 8f)
            {
                // Ignore the Y axis.
                this.transform.LookAt(new Vector3(player.position.x, this.transform.position.y, player.position.z));
            }
        }
    }

    public void playerEnter()
    {
        playerInSight = true;
    }

    public void playerExit()
    {
        playerInSight = false;
        moveToNextWaypoint();
    }

    public void arriveToWaypoint()
    {
        animator.SetBool("IsWalking", false);
        navMeshAgent.updateRotation = false;
        waitTime = Random.Range(4.0f, 8.0f);
        m_CurrentWayPointIndex = (m_CurrentWayPointIndex + 1) % waypoints.Length;

        Invoke("moveToNextWaypoint", waitTime);
    }

    public void moveToNextWaypoint()
    {
        if (navMeshAgent.isStopped == true)
        {
            navMeshAgent.isStopped = false;
        }

        navMeshAgent.updateRotation = true;
        animator.SetBool("IsWalking", true);
        navMeshAgent.SetDestination(waypoints[m_CurrentWayPointIndex].position);
    }
}