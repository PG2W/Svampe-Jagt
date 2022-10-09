using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 6f;

    [SerializeField]
    private float mouseSensitivity = 100f;

    [SerializeField]
    [
        Header(
            "The maximum angle the player can look up and or down (in degrees)")
    ]
    private float maxLookAngle = 90f;
    public Transform head;


    private float xRotation = 0f;


    void Start()
    {
    }

    void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        transform.position += movement * movementSpeed * Time.deltaTime;
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
