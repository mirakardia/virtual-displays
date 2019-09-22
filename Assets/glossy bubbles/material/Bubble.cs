using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    public GameObject FloatingText;

    public Transform onHitParticle;

    public delegate void BubbleEventHandler(int score);
    public static event BubbleEventHandler OnBubbleHit;

    // Start is called before the first frame update
    // void Start()

    public int score = 0;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < 7.15f)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        float yVel = gameObject.GetComponent<Rigidbody>().velocity.y + Physics.gravity.y;

        //Hovering
        gameObject.GetComponent<Rigidbody>().AddForce(0, -yVel * 0.7f, 0, ForceMode.Acceleration);

        //Altitude
        gameObject.GetComponent<Rigidbody>().AddForce(0, Input.GetAxis("Vertical") * 5, 0);
    }

    void OnCollisionEnter(Collision collision) {

        if (collision.collider.tag.Equals("Player"))
        {
            this.GetComponent<Renderer>().enabled = false;
            this.GetComponent<Collider>().enabled = false;

            GameObject floatingText = Instantiate(FloatingText, transform.position, Quaternion.identity, transform.parent);
            floatingText.GetComponent<TextMesh>().text = "+" + score;
            floatingText.transform.localPosition = transform.position + Vector3.up * 0.25f;

            if (OnBubbleHit != null)
            {
                OnBubbleHit(score);
            }

            if (onHitParticle != null)
            {
                Instantiate(onHitParticle, transform.position, Quaternion.identity, transform);
            }

            Destroy(gameObject, 0.5f);
        }
    }
}
