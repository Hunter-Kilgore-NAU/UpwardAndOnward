using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Add this for scene management

public class CharacterMovement : MonoBehaviour
{
    CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;

    Vector3 move;
    Vector3 input;
    Vector3 yVelocity;

    int jumpCharges = 1;

    bool isGrounded;
    bool isSprinting;
    bool isCrouching;

    float speed;
    public float runSpeed;
    public float airSpeed;
    public float sprintSpeed;
    public float crouchSpeed;

    float gravity;
    public float normalGravity;
    public float jumpHeight;

    float startHeight;
    float crouchHeight = 0.5f;

    Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    Vector3 standingCenter = new Vector3(0, 0, 0);

    public Vector3 respawnPoint;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        startHeight = transform.localScale.y;

        // Ensure the cursor is hidden and locked when the game starts
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void HandleInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        input = transform.TransformDirection(input);
        input = Vector3.ClampMagnitude(input, 1f);

        if (Input.GetKeyUp(KeyCode.Space) && jumpCharges > 0)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            ExitCrouch();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            isSprinting = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && isGrounded)
        {
            isSprinting = false;
        }

        // Check if the ESC key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }
    }

    void Update()
    {
        HandleInput();

        if (isGrounded)
        {
            GroundedMovement();
        }
        else
        {
            AirMovement();
        }

        checkGround();

        controller.Move(move * Time.deltaTime);

        ApplyGravity();

        if (transform.position.y < -60)
        {
            Respawn();
        }
    }

    void GroundedMovement()
    {
        speed = isSprinting ? sprintSpeed : isCrouching ? crouchSpeed : runSpeed;

        if (input.x != 0)
        {
            move.x += input.x * speed;
        }
        else
        {
            move.x = 0;
        }

        if (input.z != 0)
        {
            move.z += input.z * speed;
        }
        else
        {
            move.z = 0;
        }

        move = Vector3.ClampMagnitude(move, speed);
    }

    void AirMovement()
    {
        move.x += input.x * airSpeed;
        move.z += input.z * airSpeed;
        move = Vector3.ClampMagnitude(move, speed);
    }

    void Jump()
    {
        if (!isGrounded)
        {
            jumpCharges -= 1;
        }

        yVelocity.y = Mathf.Sqrt(jumpHeight * -2f * normalGravity);
    }

    void checkGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask);

        if (isGrounded)
        {
            jumpCharges = 1;
        }
    }

    void ApplyGravity()
    {
        gravity = normalGravity;
        yVelocity.y += gravity * Time.deltaTime;
        controller.Move(yVelocity * Time.deltaTime);
    }

    void Crouch()
    {
        controller.height = crouchHeight;
        controller.center = crouchingCenter;

        transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);

        isCrouching = true;
    }

    void ExitCrouch()
    {
        controller.height = startHeight * 2;
        controller.center = standingCenter;

        transform.localScale = new Vector3(transform.localScale.x, startHeight, transform.localScale.z);

        isCrouching = false;
    }

    void Respawn()
    {
        transform.position = respawnPoint;
        yVelocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            respawnPoint = other.transform.position;
        }
    }

    public void SetVerticalVelocity(float newYVelocity)
    {
        yVelocity.y = newYVelocity;
    }

    // Method to return to the main menu
    void ReturnToMainMenu()
    {
        // Unlock the cursor and make it visible when returning to the main menu
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");  // Replace with the actual scene name for the main menu
    }
}
