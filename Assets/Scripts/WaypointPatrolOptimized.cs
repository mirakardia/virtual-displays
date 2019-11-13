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
    bool atWaypoint;            // If player is at a waypoint
    float waypointIdleTime;     // How long virtual agent waits at a waypoint

    void Start()
    {
        animator = GetComponent<Animator>();

        navMeshAgent.SetDestination(waypoints[0].position);
        animator.SetBool("IsWalking", true);

        waypointIdleTime = Random.Range(4.0f, 8.0f);
        atWaypoint = false;
    }

    // Turn toward player
    IEnumerator TurnTowardPlayer()
    {
        while (playerInSight)
        {
            // Ignore the Y axis.
            this.transform.LookAt(new Vector3(player.position.x, this.transform.position.y, player.position.z));
            yield return null;
        }
    }

    // Stop moving and turn toward player when player is in line of sight
    public void PlayerEnterLineOfSight()
    {
        playerInSight = true;

        if (IsInvoking("MoveToNextWaypoint"))
        {
            CancelInvoke();
        }

        if (animator.GetBool("IsWalking") == true)
        {
            animator.SetBool("IsWalking", false);
            navMeshAgent.isStopped = true;
        }

        StartCoroutine(TurnTowardPlayer());
    }

    // Continue walking when player exits line of sight or when enough time has passed
    public void PlayerExit()
    {
        playerInSight = false;
        StopCoroutine(TurnTowardPlayer());

        if (atWaypoint){
            Invoke("MoveToNextWaypoint", waypointIdleTime);
        }
        else
        {
            MoveToNextWaypoint();
        }
    }

    // Virtual agent arrives to waypoint
    public void ArriveToWaypoint()
    {
        if (navMeshAgent.remainingDistance < 1.5 && !atWaypoint)
        {
            atWaypoint = true;

            animator.SetBool("IsWalking", false);
            navMeshAgent.updateRotation = false;

            waypointIdleTime = Random.Range(4.0f, 8.0f);
            m_CurrentWayPointIndex = (m_CurrentWayPointIndex + 1) % waypoints.Length;

            // Move to next waypoint after wait time
            Invoke("MoveToNextWaypoint", waypointIdleTime);
        }
    }

    // Virtual agent moves to next waypoint
    public void MoveToNextWaypoint()
    {

        if (navMeshAgent.isStopped == true)
        {
            navMeshAgent.isStopped = false;
        }

        atWaypoint = false;
        navMeshAgent.updateRotation = true;

        animator.SetBool("IsWalking", true);
        navMeshAgent.SetDestination(waypoints[m_CurrentWayPointIndex].position);
    }
}