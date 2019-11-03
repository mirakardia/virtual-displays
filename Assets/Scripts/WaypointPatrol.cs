using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
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
        Debug.Log(navMeshAgent.remainingDistance);

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

        else if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
        {
            if (animator.GetBool("IsWalking") == true)
            {
                Debug.Log(navMeshAgent.remainingDistance);
                arriveToWaypoint();
            }
            
            else if (Time.time > arrivalTime + waitTime)
            {
                moveToNextWaypoint();
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

    private void arriveToWaypoint()
    {
        Debug.Log("arrived at " + m_CurrentWayPointIndex);
        animator.SetBool("IsWalking", false);
        waitTime = Random.Range(4.0f, 8.0f);
        arrivalTime = Time.time;
        m_CurrentWayPointIndex = (m_CurrentWayPointIndex + 1) % waypoints.Length;
    }

    private void moveToNextWaypoint()
    {
        if(navMeshAgent.isStopped == true)
        {
            navMeshAgent.isStopped = false;
        }

        Debug.Log("going to " + m_CurrentWayPointIndex);

        animator.SetBool("IsWalking", true);
        navMeshAgent.SetDestination(waypoints[m_CurrentWayPointIndex].position);
    }
}
