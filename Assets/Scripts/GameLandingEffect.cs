using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLandingEffect : MonoBehaviour
{
    public GameObject[] gameElements;
    public Text text;

    public Text instructions;
    public float instructionsDuration;

    private int players = 0;
    private int score = 0;
    private int bubblesHit = 0;

    float timeLeft = 1f;
    float FloatStrength = 1f;
    float RandomRotationStrength = 2;

    public float topEdge, bottomEdge, leftEdge, rightEdge;
    public float positionZ;
    private float verticalOffset = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        Bubble.OnBubbleHit += OnBubbleHit;
        BodySourceView.OnPlayerEntered += PlayerEntered;
        BodySourceView.OnPlayerExited += PlayerExited;

        CreateRandomGameElement();

        hideInstructions();

        topEdge = this.GetComponent<MeshRenderer>().bounds.center.y + this.GetComponent<MeshRenderer>().bounds.size.y / 2;
        bottomEdge = this.GetComponent<MeshRenderer>().bounds.center.y - this.GetComponent<MeshRenderer>().bounds.size.y / 2;
        leftEdge = this.GetComponent<MeshRenderer>().bounds.center.x - this.GetComponent<MeshRenderer>().bounds.size.x / 2;
        rightEdge = this.GetComponent<MeshRenderer>().bounds.center.x + this.GetComponent<MeshRenderer>().bounds.size.x / 2;

        positionZ = this.GetComponent<MeshRenderer>().bounds.center.z - 0.01f;

    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Score: " + score;
    }

    private void FixedUpdate()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            CreateRandomGameElement();
        }
    }

    private void CreateRandomGameElement()
    {
        float x = Random.Range(leftEdge + 0.2f, rightEdge - 0.2f);
        int choice = (int)Random.Range(0, gameElements.Length - 1);
        GameObject tmp;

        tmp = Instantiate(gameElements[choice], new Vector3(x, topEdge - verticalOffset, positionZ), Quaternion.identity);
        tmp.transform.GetComponent<Rigidbody>().AddForce(Vector3.up * FloatStrength);
        tmp.transform.Rotate(RandomRotationStrength, RandomRotationStrength, 0);

        timeLeft = Random.Range(1f, 1.5f);
    }

    private void OnBubbleHit(int _score)
    {
        Logger.Log("KINECT_GAME", "Bubble hit, points received: " + _score);

        score += _score;
        bubblesHit++;
    }

    private void PlayerEntered(ulong id)
    {
        Logger.Log("KINECT_GAME", "Player entered: " + id);

        if (players == 0 && instructions != null)
        {
            instructions.enabled = true;
            Invoke("hideInstructions", instructionsDuration);
        }

        players++;
    }

    private void PlayerExited(ulong id)
    {
        players--;
        if (players < 1)
        {
            Logger.Log("KINECT_GAME", "Player exited: " + id);
            Logger.Log("KINECT_GAME", "Total score: " + score);
            Logger.Log("KINECT_GAME", "Total bubbles hit: " + bubblesHit);
            score = 0;
            bubblesHit = 0;
        }
    }

    private void hideInstructions()
    {
        instructions.enabled = false;
    }
}
