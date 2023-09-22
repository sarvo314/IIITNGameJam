using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public static event Action OnStartCutscene;
    public static event Action OnEndCutscene;

    private void OnEnable()
    {
        OnStartCutscene += DisableMovement;
        OnEndCutscene += EnableMovement;
    }

    [SerializeField]
    private float speed = 4f;
    private float jumpForce = 10f;
    private float sprintModifier = 1.5f;
    private float gravity = -9.81f;
    private Rigidbody rb;
    private bool isGrounded;
    [SerializeField]
    private CharacterController controller;
    private Vector3 velocity;
    private bool isMoveable;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity = Vector3.zero;
        isMoveable = true;

    }


    void FixedUpdate()
    {
        if (isMoveable) Move();
    }

    private void Move()
    {
        //Get the player's input

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        // Check if the player is sprinting
        bool sprinting = Input.GetKey(KeyCode.LeftShift);

        // Calculate the player's movement direction
        //Vector3 direction = new Vector3(x, 0, y);
        Vector3 direction = transform.right * x + transform.forward * z;
        direction.Normalize();

        // Apply the player's movement speed
        float adjustedSpeed = speed;
        if (sprinting)
        {
            adjustedSpeed *= sprintModifier;
        }
        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y += jumpForce * Time.deltaTime;
        }

        controller.Move(direction * adjustedSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        // Check if the player is grounded
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.5f))
        {
            isGrounded = true;
            velocity.y = 0;
        }
        else
        {
            isGrounded = false;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    private void DisableMovement()
    {
        isMoveable = false;
    }
    private void EnableMovement()
    {
        isMoveable = true;
    }
    private void OnDisable()
    {
        OnEndCutscene -= EnableMovement;
        OnStartCutscene -= DisableMovement;
    }
}