using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class FloatingTextScript : MonoBehaviour
{
    public float lifeTime;
    private float timeElapsed;

    public float verticalMovement;

    private TextMesh text;
    private Color startColor;
    private Color endColor;

    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<TextMesh>();
        startColor = text.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        timeElapsed = 0f;

        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        text.color = Color.Lerp(startColor, endColor, timeElapsed / lifeTime);

        transform.position = transform.position + Vector3.up * verticalMovement * Time.deltaTime;
    }
}
