using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    private float moveSpeed = 5f;
    public float groundDrag = 5f;
    public float jumpForce = 10f;
    public float jumpCooldown = 0.5f;
    public float airMultiplier = 0.5f;
    private bool readyToJump = true;
    public float walkSpeed = 100f;
    public float sprintSpeed = 150f;
    public float crouchSpeed = 50f;

    [Header("Crouch Settings")]
    public float crouchHeight = 1f; // Desired collider height when crouched
    private float originalHeight;
    public Vector3 crouchCenter = new Vector3(0, -0.5f, 0); // Adjust based on your model
    private Vector3 originalCenter;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.7f;
    public LayerMask whatIsGround;
    private bool grounded;

    [Header("Orientation")]
    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        capsuleCollider = GetComponent<CapsuleCollider>();
        if (capsuleCollider != null)
        {
            originalHeight = capsuleCollider.height;
            originalCenter = capsuleCollider.center;
        }
    }

    private void Update()
    {
        // Check if player is grounded using a sphere at groundCheck position
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        // Apply ground drag if on ground, zero if in air
        rb.linearDamping = grounded ? groundDrag : 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();

        // Optional downward force to keep player from "floating" on slopes
        if (!grounded)
        {
            rb.AddForce(Vector3.down * 20f, ForceMode.Force);
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Reworked Jump Logic: trigger once per key press
        if (Input.GetKeyDown(jumpKey) && grounded && readyToJump)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // Crouching logic
        if (Input.GetKeyDown(crouchKey))
        {
            StartCrouch();
        }
        if (Input.GetKeyUp(crouchKey))
        {
            StopCrouch();
        }
    }

    private void StartCrouch()
    {
        // Adjust collider parameters for crouching
        if (capsuleCollider != null)
        {
            capsuleCollider.height = crouchHeight;
            capsuleCollider.center = crouchCenter;
        }
        // Optionally, add a downward impulse to ensure contact with the ground
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
    }

    private void StopCrouch()
    {
        // Reset collider parameters
        if (capsuleCollider != null)
        {
            capsuleCollider.height = originalHeight;
            capsuleCollider.center = originalCenter;
        }
    }

    private void StateHandler()
    {
        // Crouching has priority
        if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }
        // Sprinting
        else if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        // Walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        // Air
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        // Determine direction based on orientation
        moveDirection = (orientation.forward * verticalInput + orientation.right * horizontalInput).normalized;

        // Apply different force if grounded or in air
        if (grounded)
            rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        // Limit the player's speed to moveSpeed
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // Reset vertical velocity and apply jump force
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void OnDrawGizmos()
    {
        // Visualize the ground check in Scene view
        if (groundCheck)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }

    void OnGUI()
    {
        // Simple debug label to see if the player is grounded
        GUI.Label(new Rect(10, 10, 200, 20), grounded ? "Grounded" : "Not Grounded");
    }
}
