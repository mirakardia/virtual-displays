using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public Transform player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
        {
            Debug.Log("pelaaja spotattu'd");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform == player)
        {
            Debug.Log("pelaaja kadotettu'd");
        }
        
    }
}
