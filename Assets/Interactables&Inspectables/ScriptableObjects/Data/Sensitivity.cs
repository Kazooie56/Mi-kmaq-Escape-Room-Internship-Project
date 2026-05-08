using UnityEngine;

[CreateAssetMenu(menuName = "Sensitivity/Sensitivity Settings")]
public class InputSettings : ScriptableObject
{
    // ADJUST THIS NUMBER IF YOUR MOUSE SENSITIVITY IS TOO HIGH OR LOW
    public float lookSensitivity = 400f;
}