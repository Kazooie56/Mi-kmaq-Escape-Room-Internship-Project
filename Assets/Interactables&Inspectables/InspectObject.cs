using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class InspectObject : MonoBehaviour,IInspectable
{
    [SerializeField] private InspectData InspectData;
    private float movementDuration;

    private Transform originalParent;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private Rigidbody rb;
    private Transform playerCameraTransform;
    private Vector3 heldOffsetPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCameraTransform = Camera.main.transform;
        if (InspectData != null)
        {
            heldOffsetPosition = InspectData.holdPointOffset;
            movementDuration = InspectData.movementDuration;
        }
    }

    public void OnInspectStart()
    {
        rb.isKinematic = true;
        originalParent = transform.parent;
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;

        Vector3 targetInspectPosition = playerCameraTransform.position + playerCameraTransform.TransformDirection(heldOffsetPosition);
        StartCoroutine(MoveAndRotateTo(targetInspectPosition, transform.rotation));
        //transform.position = targetInspectPosition;
    }

    public void OnInspectEnd()
    {
        rb.isKinematic = false;
        transform.SetParent(originalParent);
        StartCoroutine(MoveAndRotateTo(originalPosition, originalRotation));

        //transform.position = originalPosition;
        //transform.rotation = originalRotation;
    }
    IEnumerator MoveAndRotateTo(Vector3 targetPosition, Quaternion targetRotation)
    {
        float time = 0f;
        float duration = movementDuration;

        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        while (time < duration)
        {
            float progress = time / duration;

            transform.position = Vector3.Lerp(startPos, targetPosition, progress);
            transform.rotation = Quaternion.Lerp(startRot, targetRotation, progress);

            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }

}
