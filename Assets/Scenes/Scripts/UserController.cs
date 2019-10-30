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

        // Put debug shortcuts behind modifier key (leftShift).
        if(Input.GetKey(KeyCode.LeftShift)) {
            if (Input.GetKeyDown("escape")) {
                if (Cursor.lockState != CursorLockMode.None) {
                    Cursor.lockState = CursorLockMode.None;
                    Debug.Log("Shift + Esc disengages user controls.");
                } else {
                    Cursor.lockState = CursorLockMode.Locked;
                    Debug.Log("Shift + Esc engages user controls.");
                }
            }
        }
    }
}
