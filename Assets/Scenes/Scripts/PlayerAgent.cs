using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerAgent : MonoBehaviour
{
    public Transform Destination1;
    public Transform Destination2;
    public Transform Destination3;
    public GameObject Questionnaire;
    public GameObject AvatarQuestionnaire;

    public Transform player;

    private NavMeshAgent navMeshAgent;

    private delegate void DestinationEventHandler();
    private event DestinationEventHandler onDestinationReached;
    private event DestinationEventHandler onFirstDestinationReached;

    private bool lookAtPlayer;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.SetDestination(Destination1.position);
        onDestinationReached += FirstDestinationReached;

        lookAtPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDestinationReached();

        if (lookAtPlayer)
        {
            TurnTowardsPlayer();
        }
    }

    bool CheckDestinationReached()
    {
        if (!navMeshAgent.isStopped && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (onDestinationReached != null)
            {
                onDestinationReached();
            }
            navMeshAgent.isStopped = true;
            return true;
        }
        else
        {

            return false;
        }
    }

    private void TurnTowardsPlayer()
    {
        // Look at the player.
        if (player && Vector3.Distance(player.position, this.transform.position) >= 8f)
        {
            // Ignore the Y axis.
            this.transform.LookAt(new Vector3(player.position.x, this.transform.position.y, player.position.z));
        }
    }

    private void FirstDestinationReached()
    {
        Debug.Log("FirstDestinationReached");

        GetComponent<Animator>().SetBool("Reached", true);
        onDestinationReached -= FirstDestinationReached;

        Invoke("ShowQuestionnaire", 1f);
        Invoke("setSecondDestination", 3f);
    }

    private void ShowQuestionnaire()
    {
        Debug.Log("ShowQuestionnaire");

        Questionnaire.SetActive(true);
        AvatarQuestionnaire.SetActive(false);
    }

    private void setSecondDestination()
    {
        Debug.Log("setSecondDestination");

        GetComponent<Animator>().SetBool("Reached", false);
        navMeshAgent.SetDestination(Destination2.position);
        navMeshAgent.isStopped = false;
        onDestinationReached += SecondDestinationReached;
    }

    private void SecondDestinationReached()
    {
        Debug.Log("SecondDestinationReached");

        GetComponent<Animator>().SetBool("Reached", true);
        onDestinationReached -= SecondDestinationReached;

        Invoke("startLookingAtPlayer", 1f);
        //Invoke("setThirdDestination", 15f);
    }

    private void startLookingAtPlayer()
    {
        lookAtPlayer = true;
    }

    private void setThirdDestination()
    {
        Debug.Log("setThirdDestination");

        lookAtPlayer = false;

        GetComponent<Animator>().SetBool("Reached", false);
        navMeshAgent.SetDestination(Destination3.position);
        navMeshAgent.isStopped = false;
        onDestinationReached += ThirdDestinationReached;
    }

    private void ThirdDestinationReached()
    {
        Debug.Log("ThirdDestinationReached");

        GetComponent<Animator>().SetBool("Reached", true);
        onDestinationReached -= ThirdDestinationReached;

        Destroy(this.gameObject, 2f);
    }

}
