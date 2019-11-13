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
            waypointPatrol.PlayerEnterLineOfSight();
            StartCoroutine(AutomaticExit());
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform == player)
        {
            StopCoroutine(AutomaticExit());
            waypointPatrol.PlayerExit();
        }
    }

    IEnumerator AutomaticExit()
    {

        yield return new WaitForSeconds(8);

        GetComponent<Collider>().enabled = false;
        Debug.Log("Automatic exit");
        waypointPatrol.PlayerExit();

        yield return new WaitForSeconds(10);
        GetComponent<Collider>().enabled = true;
    }
}
