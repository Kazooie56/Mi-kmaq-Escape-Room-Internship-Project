using UnityEngine;
[CreateAssetMenu(fileName = "PickUpData", menuName = "ScriptableData/PickUp")]
public class PickUpData : ScriptableObject
{
    [Tooltip("This will be how fast the object moves to the held point position")]
    public float lerpMoveSpeed;
    [Tooltip("This is how far the object will be held from the camera position")]
    public Vector3 holdPointOffset;
}
