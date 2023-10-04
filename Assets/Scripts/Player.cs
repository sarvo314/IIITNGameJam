using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    public static event Action playerDeadSequence;

    public static float health;
    public static bool playerIsHitting;
    private float MAX_HEALTH = 100;
    private float MIN_HEALTH = 0;
    [SerializeField]
    private Slider playerHealthSlider;
    [SerializeField]
    private Animator animator;

    public Camera playerCamera;

    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    float rotationX = 0;


    public float gravity = 20.0f;
    [HideInInspector]
    public bool canMove = true;

    public float walkingSpeed = 7.5f;

    public float runningSpeed = 9f;

    private const string IS_RUNNING = "isRunning";

    [SerializeField]
    private float punchRaycastDistance = 2f;

    [SerializeField]
    AudioManager audioManager;

    [SerializeField]
    GameInput gameInput;

    public float jumpSpeed = 8.0f;
    //damage by punches
    [SerializeField]
    private float damage = 5f;

    CharacterController characterController;
    private void OnEnable()
    {
        playerDeadSequence += Die;
    }

    Vector3 moveDirection = Vector3.zero;

    //string[] combatMove = { "Punch", "PunchToElbow" }
    Dictionary<int, KeyValuePair<string, float>>
          combatMove = new Dictionary<int, KeyValuePair<string, float>>();
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        health = MAX_HEALTH;
        playerHealthSlider.value = health;
        playerIsHitting = false;
        gameInput.OnJumpAction += GameInput_OnJumpAction;
        gameInput.OnPunchAction += GameInput_OnPunchAction;
        Moves();
    }

    private void GameInput_OnPunchAction(object sender, EventArgs e)
    {
        while (Enemy.enemyIsHitting) ;
        //audioManager.PlayPunchSound();
        playerIsHitting = true;
        int move = ChooseRandomMove();
        string moveName = combatMove[move].Key;
        animator.SetTrigger(moveName);
        playerIsHitting = false;
    }

    private void GameInput_OnJumpAction(object sender, EventArgs e)
    {
        if (characterController.isGrounded)
            moveDirection.y = jumpSpeed;
        animator.SetTrigger("Jump");
    }

    private void Moves()
    {
        combatMove.Add(0, new KeyValuePair<string, float>("Punch", 5f));
    }

    private void Update()
    {
        HandleMovement();
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 movDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedHorizontal = canMove ? (isRunning ? runningSpeed : walkingSpeed) * movDir.x : 0;
        float curSpeedVertical = canMove ? (isRunning ? runningSpeed : walkingSpeed) * movDir.z : 0;
        float movementDirectionUp = moveDirection.y;
        ///as we are initializing move direction again we have to save the previous y value that's why it wasn't working
        moveDirection = (forward * curSpeedVertical) + (right * curSpeedHorizontal);

        //to save the y position while jumping
        moveDirection.y = movementDirectionUp;

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        if (curSpeedHorizontal > 0 || curSpeedVertical > 0)
        {
            ToggleRunningAnimation(true);
        }
        else
        {
            ToggleRunningAnimation(false);
        }
        animator.SetFloat("Speed", Mathf.Sqrt(curSpeedVertical * curSpeedVertical + curSpeedHorizontal * curSpeedHorizontal));
        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -gameInput.GetLookVectorNormalized().y * lookSpeed;

            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            transform.rotation *= Quaternion.Euler(0, gameInput.GetLookVectorNormalized().x * lookSpeed, 0);
            //rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        }
    }

    private void ToggleRunningAnimation(bool state)
    {
        animator.SetBool(IS_RUNNING, state);
    }

    private int ChooseRandomMove()
    {
        return UnityEngine.Random.Range(0, combatMove.Count);
    }

    //triggered from animation
    public void EnemyDamage(float moveDamage)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, punchRaycastDistance))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
                enemy.Damage(moveDamage);
                audioManager.PlayPunchSound();
                audioManager.PlayGetHitSound();
                enemy.showHitReaction();
            }
        }
    }

    public void Heal(float heal)
    {
        health = Mathf.Min(MAX_HEALTH, health + heal);
        playerHealthSlider.value = health / MAX_HEALTH;
    }

    public void Damage(float damage)
    {

        health = Mathf.Max(MIN_HEALTH, health - damage);
        playerHealthSlider.value = health / MAX_HEALTH;
        if (health == 0)
        {
            playerDeadSequence?.Invoke();
        }
#if DEBUG
        Debug.Log("Player health is reduced");
#endif
    }
    private void Die()
    {
        animator.SetTrigger("Dead");
        SceneManager.LoadScene(0);
    }
    private void OnDisable()
    {
        playerDeadSequence -= Die;
    }
}
