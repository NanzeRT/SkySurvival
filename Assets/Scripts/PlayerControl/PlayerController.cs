using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraAnchor;
    [SerializeField]
    private TouchField touchField;
    [SerializeField]
    private Joystick joystick;
    [SerializeField]
    private TouchButton jumpButton;
    public float touchSensitivity = .2f;
    public float mouseSensitivity = 1f;
    public float xSensitivity = 1f;
    public float ySensitivity = 1f;
    public float speed = 1f;
    public float jumpSpeed = 3f;
    public float gravity = 9.81f;
    public float jumpGravity = 3f;
    public float jumpTime = 1f;
    private CharacterController controller;

    private float yLookAngle;
    private float yVelocity = 0f;
    private float lastJump;

    private Vector3 moveVector;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        yLookAngle = cameraAnchor.transform.localRotation.eulerAngles.x;
    }

    void Update()
    {
        moveVector = Vector3.zero;
        HandleMouseMovement();
        HandleMovement();
        HandleJumpAndGravity();
        if (moveVector != Vector3.zero)
            controller.Move(moveVector * Time.deltaTime);
    }

    void HandleMouseMovement()
    {
        Vector2 mouse_movement = touchField.deltaPos * touchSensitivity;
        if (mouse_movement == Vector2.zero)
            mouse_movement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseSensitivity;
        if (mouse_movement == Vector2.zero)
            return;

        yLookAngle = Mathf.Clamp(yLookAngle - mouse_movement.y * ySensitivity, -90, 90);
        cameraAnchor.transform.localRotation = Quaternion.Euler(yLookAngle, 0, 0);

        this.transform.Rotate(0, mouse_movement.x * xSensitivity, 0);
    }

    void HandleMovement()
    {
        Vector2 input = joystick.Direction;
        if (input == Vector2.zero)
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (input == Vector2.zero)
            return;

        input = input.normalized * Mathf.Max(Mathf.Abs(input.x), Mathf.Abs(input.y)) * speed;

        moveVector += transform.forward * input.y + transform.right * input.x;
    }

    void HandleJumpAndGravity()
    {
        if (jumpButton.Pressed || Input.GetButton("Jump"))
        {
            if (controller.isGrounded)
            {
                yVelocity = jumpSpeed;
                lastJump = Time.time;
            }

            if (Time.time - lastJump < jumpTime)
            {
                ApplyGravity(jumpGravity);
                return;
            }
        }

        if (controller.isGrounded)
        {
            yVelocity = 0f;
            return;
        }
        
        ApplyGravity(gravity);
    }

    void ApplyGravity(float g)
    {
        moveVector += Vector3.up * yVelocity;
        yVelocity -= g * Time.deltaTime;
    }
}
