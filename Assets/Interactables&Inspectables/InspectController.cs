using UnityEngine;

public class InspectController : MonoBehaviour
{

    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask inspectMask;
    [SerializeField] private float inspectRotationSpeed;
    [SerializeField] private float raycastDistance;

    private IInspectable inspectable;

    // The transform of the inspected object for rotation
    private Transform inspectTransform;

    private bool isInspecting;

    // Player controller to disable movement, will probably need to change to newer version
    //private FirstPersonController inputs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCameraTransform = Camera.main.transform;
        //inputs = GetComponent<FirstPersonController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isInspecting)
            {
                StopInspect();
            }
            else
            {
                RaycastInspect();
            }
        }

        if (isInspecting)
        {
            RotateObject();
        }
    }
    /// <summary>
    /// Shoots a raycast for inspectable object. Calls Inspect start aswell as get inspect transform and bool isInspecting is true
    /// </summary>
    void RaycastInspect()
    {
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit hitObject, raycastDistance, inspectMask))
        {
            if (hitObject.transform.TryGetComponent<IInspectable>(out IInspectable newInspectable))
            {
                inspectable = newInspectable;
                inspectTransform = hitObject.transform;
                isInspecting = true;

                //if (inputs != null) inputs.enabled = false;

                
                if (inspectable != null)
                {
                    inspectable.OnInspectStart();
                }
            }
        }
    }

    void RotateObject()
    {
        // All this will probably change to new input system so this is kinda of a place holder code
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        inspectTransform.Rotate(playerCameraTransform.up, -mouseX * inspectRotationSpeed * Time.deltaTime, Space.World);
        inspectTransform.Rotate(playerCameraTransform.right, mouseY * inspectRotationSpeed * Time.deltaTime, Space.World);
    }

    void StopInspect()
    {
        if (inspectTransform == null) return;
        //if (inputs != null) inputs.enabled = true;

        if (inspectable != null) 
        {
            inspectable.OnInspectEnd();
        }
        inspectable = null;
        inspectTransform = null;
        isInspecting = false;
    }
}