using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public Transform player;

    // Waypoint patrol needs to know if a player is spotted
    public WaypointPatrolOptimized waypointPatrol;


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
        {
            waypointPatrol.playerEnterLineOfSight();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform == player)
        {
            waypointPatrol.playerExit();
        }
    }
}
