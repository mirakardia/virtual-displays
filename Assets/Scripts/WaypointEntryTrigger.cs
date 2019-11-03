using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointEntryTrigger : MonoBehaviour
{
    public Transform virtualAgent;

    // Waypoint patrol needs to know when a waypoint is reached
    public WaypointPatrolOptimized waypointPatrol;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == virtualAgent)
        {
            waypointPatrol.arriveToWaypoint();
        }

    }
}
