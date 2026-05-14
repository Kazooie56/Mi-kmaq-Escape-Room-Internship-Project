using UnityEngine;
[CreateAssetMenu(fileName = "Light Data", menuName = "Puzzles/Temperature Ice Cube/Lighting Config")]
public class LightingConfig : ScriptableObject
{
    public Color colorLight;

    public float range;

    public float intensity;

    public float innerSpotAngle;

    public float spotAngle;

    private void OnValidate()
    {
        if (innerSpotAngle > spotAngle)
        {
            Debug.Log("inner spot angle is outside the spot angle ");
            innerSpotAngle = spotAngle;
        }
    }
}
