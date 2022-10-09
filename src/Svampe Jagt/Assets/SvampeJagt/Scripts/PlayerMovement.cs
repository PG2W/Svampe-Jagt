using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 6f;

    [SerializeField]
    private float mouseSensitivity = 1000f;

    [SerializeField]
    [
        Header(
            "The maximum angle the player can look up and or down (in degrees)")
    ]
    private float maxLookAngle = 90f;

    public Transform head;

    private float xRotation = 0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement =
            transform.right * horizontal + transform.forward * vertical;

<<<<<<< Updated upstream
        transform.position += movement * movementSpeed * Time.deltaTime;

=======
        rb.AddForce(movement * movementSpeed * 1000f * Time.deltaTime);
>>>>>>> Stashed changes
    }

    private void RotatePlayer()
    {
        float mouseX =
            Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY =
            Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);

        head.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }
}
