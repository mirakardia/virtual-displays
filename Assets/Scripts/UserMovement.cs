using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UserMovement : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 inputVelocity)
    {
        velocity = inputVelocity;
    }

    void FixedUpdate()
    {
        executeMovement();
    }

    void executeMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }
}
