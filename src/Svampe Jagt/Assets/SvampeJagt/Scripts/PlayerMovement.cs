using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField]
    private float movementSpeed = 6f;

    [SerializeField]
    private float mouseSensitivity = 1000f;

    [SerializeField]
    [
        Tooltip(
            "The maximum angle the player can look up and or down (in degrees)")
    ]
    private float maxLookAngle = 90f;

    public Transform head;

    public float gravityConstant = 9.82f;

    public float jumpHeight = 1f;

    public GroundChecker groundChecker;

    private float xRotation = 45f;

    private float currentVelocity = 0f;

    private bool jumpCooldown = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MovePlayer();
        RotatePlayer();
        ApplyGravity();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (groundChecker.IsGrounded)
        {
            Debug.Log("Jump");
            currentVelocity += -Mathf.Sqrt(jumpHeight * 2f * gravityConstant); //Derives from v^2=2gs
            StartCoroutine(ApplyJumpCooldown());
        }
    }

    IEnumerator ApplyJumpCooldown()
    {
        jumpCooldown = true;
        yield return new WaitForSeconds(0.5f);
        jumpCooldown = false;
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement =
            (transform.right * horizontal + transform.forward * vertical)
                .normalized;

        characterController.Move(movement * movementSpeed * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        characterController
            .Move(Vector3.down * currentVelocity * Time.deltaTime);

        if (groundChecker.IsGrounded && !jumpCooldown)
        {
            currentVelocity = 0f;
            return;
        }

        currentVelocity += gravityConstant * Time.deltaTime;
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
