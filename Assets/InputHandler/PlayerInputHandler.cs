using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour, PlayerInput.IPlayerActions
{
    
    private PlayerInput inputActions;

    private FirstPersonController fpController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActions = new PlayerInput();
        inputActions.Player.SetCallbacks(this);
        inputActions.Enable();

        fpController = GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        Debug.Log("Crouch");
    }

    public void OnInspect(InputAction.CallbackContext context)
    {
        Debug.Log("Inspect");
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interact");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (fpController.IsGrounded())
            {
                Debug.Log("I can Jump");
            }
            else
            {
                Debug.Log("I can't Jump");
            }
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        fpController.SetLook(context.ReadValue<Vector2>());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        fpController.SetMovement(context.ReadValue<Vector2>());
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        // I don't know if we need this but ill keep it just in case
        Debug.Log("Next");
    }

    public void OnPickUp(InputAction.CallbackContext context)
    {
        Debug.Log("PickUp");
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        // I don't know if we need this but ill keep it just in case
        Debug.Log("Previous");
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        Debug.Log("Sprint");
    }
}
