using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    FirstPersonData firstPersonData;
    [SerializeField] private float movementSpeed;

    [SerializeField] private float rotationSpeed;

    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float gravityMultiplier = 3f;
    [SerializeField] private float groundForce;
    

    [SerializeField] private float upLookLimit;
    [SerializeField] private float downLookLimit;

    private CharacterController characterController;

    private Vector2 movementInput;
    private Vector2 lookInput;
    private Vector3 velocity;

    // Used to rotate the camera up and down
    [SerializeField] private Transform cameraTarget;

    private float xRotation;

    private bool canLook;
    private bool canMove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        SetLookBool(true);
        SetMoveBool(true);
    }

    // Update is called once per frame
    void Update()
    {
        ApplyLook();
        ApplyMovement();
        ApplyGravity();
    }

    private void ApplyMovement()
    {
        if (!canMove) return;

        Vector3 direction = new Vector3(movementInput.x, 0f, movementInput.y);

        // Converts local direction to world position
        direction = transform.TransformDirection(direction);

        characterController.Move((direction + velocity) * movementSpeed * Time.deltaTime);
    }
    private void ApplyLook()
    {
        if (!canLook) return;

        float mouseX = lookInput.x * rotationSpeed * Time.deltaTime;
        float mouseY = lookInput.y * rotationSpeed * Time.deltaTime;

        // Rotates the player object on the y rotation
        transform.Rotate(Vector3.up * mouseX);

        // Update angle for up and down look
        xRotation -= mouseY;

        // Locking the up and down rotation 
        xRotation = Mathf.Clamp(xRotation, downLookLimit, upLookLimit);

        if (cameraTarget == null) return; 
        // Rotates the camera target up or down
        cameraTarget.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void ApplyGravity()
    {
        // Check if player is grounded and falling 
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = groundForce;
            
        }
        velocity.y -= gravity * gravityMultiplier * Time.deltaTime;
    }

    public void SetMovement(Vector2 moveValue)
    {
        movementInput = moveValue;
    }

    public void SetLook(Vector2 lookValue)
    {
        lookInput = lookValue;
    }

    public void SetLookBool(bool canlookValue)
    {
        canLook = canlookValue;
    }
    public void SetMoveBool(bool canMoveValue)
    {
        canMove = canMoveValue;
    }

    public bool IsGrounded()
    {
        if (characterController == null) return false;
        return characterController.isGrounded;
    }
}
