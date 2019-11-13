using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointEntryTrigger : MonoBehaviour
{
    // VirtualAgent that uses the waypoint
    public Transform virtualAgent;

    // WaypointPatrol needs to know when a waypoint is reached
    public WaypointPatrolOptimized waypointPatrol;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == virtualAgent)
        {
            waypointPatrol.ArriveToWaypoint();
        }

    }
}
