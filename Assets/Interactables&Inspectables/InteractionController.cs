using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public float raycastDistance;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask interactMask;

    private IInteractable interactable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Cancels old interactable if needed to cancel
            if (interactable != null && interactable is ICancel cancelInteract)
            {
                cancelInteract.OnCancel();
                interactable = null;
            }
            else
            {
                RaycastInteractable();
            }
        }
    }

    void RaycastInteractable()
    {
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit hitObject, raycastDistance, interactMask))
        {
            if (hitObject.transform.TryGetComponent<IInteractable>(out IInteractable newInteractable))
            {
                interactable = newInteractable;

                interactable.OnInteract(gameObject);
            }
        }
    }
}
