using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnJumpAction;
    public event EventHandler OnPunchAction;
    [HideInInspector]
    public bool canMove = true;

    PlayerInputActions playerInputActions;
    [SerializeField]
    private Animator animator;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump_performed;
        playerInputActions.Player.Punch.performed += Punch_performed;
    }

    private void Punch_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPunchAction?.Invoke(this, EventArgs.Empty);
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpAction?.Invoke(this, EventArgs.Empty);

    }

    public float walkingSpeed = 7.5f;
    public float runningSpeed = 9f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    void Start()
    {


        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        return inputVector;
    }
    public Vector2 GetLookVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Look.ReadValue<Vector2>();
        Debug.Log(inputVector);
        return inputVector;
    }
}