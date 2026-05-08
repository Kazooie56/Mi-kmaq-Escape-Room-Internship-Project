using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem; // needed for us to use our public InputActionReferences like moveAction and jumpAction.
using UnityEngine.Rendering;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    private float playerSpeed = 5.0f;
    private float jumpHeight = 1.5f;
    private float gravityValue = -9.81f;
    public float acceleration = 25f;
    public float deceleration = 12f;

    // uses the CharacterController Component in unity, stores our velocity, and checks if we're grounded.
    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 currentHorizontalVelocity;

    // groundedPlayer is a CharacterController built in check for our jump to use.
    private bool groundedPlayer;

    [Header("Input Actions")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference crouchAction;
    public InputActionReference runAction;

    // reference the playerCamera to know how to update where forwards is.
    public Transform playerCamera;
    public Transform playerCapsule;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;

        // we want our player to move based on the direction the player is facing, X and Z. Not Y, because then they could go upwards.
        Vector3 camForward = playerCapsule.forward;
        camForward.y = 0f;

        Vector3 camRight = playerCapsule.right;
        camRight.y = 0f;

        Vector2 input = moveAction.action.ReadValue<Vector2>();

        // Translate it to the third dimension, specifically our x and y. NOTE HOW input.y IS FOR OUR Z, THAT'S BECAUSE OUR X AND Y IN A VECTOR 2 DOESNT MEAN THE SAME IN 3D SINCE Y IS NOW UP AND DOWN
        Vector3 move = camForward * input.y + camRight * input.x;
        move = Vector3.ClampMagnitude(move, 1f);

        Vector3 desiredVelocity = move * playerSpeed; // Vector3 desiredVelocity is our move (the x and y input for our movement) * playerSpeed (just a flat float variable.)

        // Accelerate or decelerate
        if (move.magnitude > 0.1f)
        {
            currentHorizontalVelocity = Vector3.MoveTowards(currentHorizontalVelocity, desiredVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            currentHorizontalVelocity = Vector3.MoveTowards(currentHorizontalVelocity, Vector3.zero, deceleration * Time.deltaTime);
        }

        if (runAction.action.IsPressed())
        {
            playerSpeed = 10f;
            controller.height = 2.0f;     // without this code, you can run at crouch height.
        }

        else if (crouchAction.action.IsPressed() && !runAction.action.IsPressed())        // this works like old valve games where crouching actaully brings your character model upwards instead of moving you downwards
        {                                                                                 // so you can jump over higher obstacles by jumping. Not intentional and scuffed in implementation but cool.
            controller.height = 1.0f;
            playerSpeed = 2.5f;
        }
        else
        {
            controller.height = 2.0f;
            playerSpeed = 5f;
        }

        // Jump Logic
        if (groundedPlayer && playerVelocity.y < 0)
            playerVelocity.y = 0f;

        if (jumpAction.action.triggered && groundedPlayer)
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);


        playerVelocity.y += gravityValue * Time.deltaTime;

        Vector3 finalMove = currentHorizontalVelocity + (playerVelocity.y * Vector3.up);    // our currentHorizontalVelocity is multiplied by acceleration, deceleration and time so it has those things now
        controller.Move(finalMove * Time.deltaTime);                                        // be careful when multiplying by Time.deltaTime multiple time, things will run either very quickly or very slowly.
    }

    public void StopMovement()
    {
        currentHorizontalVelocity = Vector3.zero;
        playerVelocity = Vector3.zero;
    }
}