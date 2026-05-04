using UnityEngine;

[CreateAssetMenu(fileName = "InspectData", menuName = "ScriptableData/Inspect")]
public class InspectData : ScriptableObject
{
    [Tooltip("This is how far the object will be held from the camera position")]
    public Vector3 holdPointOffset;

    [Tooltip("How long the object will take to move into position before snapping into place")]
    public float movementDuration;
}
