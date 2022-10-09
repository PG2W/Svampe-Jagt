using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 6f;

    [SerializeField]
    private float mouseSensitivity = 6f;


    void Start()
    {
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        transform.position += movement * movementSpeed * Time.deltaTime;

    }
}
