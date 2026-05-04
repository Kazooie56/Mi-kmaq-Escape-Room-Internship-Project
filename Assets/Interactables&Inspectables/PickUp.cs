using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PickUp : MonoBehaviour, IInteractable, ICancel
{
    [SerializeField] PickUpData pickUpData;
    private Rigidbody rb;
    private Transform playerCameraTransform;
    private Vector3 heldOffsetPosition;
    private float lerpMoveSpeed;
    private bool isHeld = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCameraTransform = Camera.main.transform;
        if (pickUpData != null ) 
        {
            heldOffsetPosition = pickUpData.holdPointOffset;
            lerpMoveSpeed = pickUpData.lerpMoveSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!isHeld) return;
        MoveToHeldPosition();
    }

    private void MoveToHeldPosition()
    {
        Vector3 targetHeldPosition = playerCameraTransform.position + playerCameraTransform.TransformDirection(heldOffsetPosition);
        Vector3 lerpTargetPosition = Vector3.Lerp(transform.position, targetHeldPosition, Time.fixedDeltaTime * lerpMoveSpeed);
        rb.MovePosition(lerpTargetPosition);
    }

    public void OnPickUp()
    {
        // Flips bool from true to false and false to true
        isHeld = !isHeld;
        if (isHeld)
        {
            rb.useGravity = false;
        }
        else
        {
            rb.useGravity = true;
        }
    }

    public void OnInteract(GameObject interactor)
    {
        OnPickUp();
    }

    public void OnCancel()
    {
        isHeld = false;
        rb.useGravity = true;
    }
}

