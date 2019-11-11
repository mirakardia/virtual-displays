using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrolOptimized : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    // Array of coordinates virtual agent travels through
    public Transform[] waypoints;
    // The player character
    public Transform player;

    Animator animator;

    int m_CurrentWayPointIndex; //Where virtual agent is going to next
    bool playerInSight;         // If virtual agent sees player
    float waypointIdleTime;     // How long virtual agent waits at a waypoint
    float playerStareTime;      // How long virtual agent stares at player
    float startTime;            // When staring has commenced

    void Start()
    {
        animator = GetComponent<Animator>();

        navMeshAgent.SetDestination(waypoints[0].position);
        animator.SetBool("IsWalking", true);
        waypointIdleTime = Random.Range(4.0f, 8.0f);
    }

    void Update()
    {
        // Turn toward player when player is in line of sight
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

    public void playerEnterLineOfSight()
    {
        playerInSight = true;
    }

    public void playerExit()
    {
        playerInSight = false;
        moveToNextWaypoint();
    }

    // Player arrives to waypoint, called by WaypointEntryTrigger.cs
    public void arriveToWaypoint()
    {
        Debug.Log("Arrived at " + m_CurrentWayPointIndex);
        Debug.Log("Dist: " + navMeshAgent.remainingDistance);

        if (navMeshAgent.remainingDistance < 1.5 && !navMeshAgent.pathPending)
        {
            animator.SetBool("IsWalking", false);
            navMeshAgent.updateRotation = false;

            waypointIdleTime = Random.Range(4.0f, 8.0f);
            m_CurrentWayPointIndex = (m_CurrentWayPointIndex + 1) % waypoints.Length;

            // Move to next waypoint after wait time
            Invoke("moveToNextWaypoint", waypointIdleTime);
        }
    }

    public void moveToNextWaypoint()
    {
        Debug.Log("Going to " + m_CurrentWayPointIndex);

        if (navMeshAgent.isStopped == true)
        {
            navMeshAgent.isStopped = false;
        }

        navMeshAgent.updateRotation = true;
        animator.SetBool("IsWalking", true);
        navMeshAgent.SetDestination(waypoints[m_CurrentWayPointIndex].position);
    }
}