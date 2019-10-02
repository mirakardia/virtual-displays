using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UserMovement))]
public class UserController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private UserMovement movement;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        movement = GetComponent<UserMovement>();
    }

    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * xMove;
        Vector3 moveVertical = transform.forward * zMove;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

        movement.Move(velocity);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
