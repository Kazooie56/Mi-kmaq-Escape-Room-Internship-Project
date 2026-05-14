using UnityEngine;

[CreateAssetMenu(fileName = "Temperature Puzzle Data", menuName = "Puzzles/Temperature Ice Cube/Puzzle Data")]
public class TemperaturePuzzleSetting : ScriptableObject
{
    [Header("Temperature Settings")]

    [Tooltip("The rate at which the temperature will go up or down")]
    [Range(.5f, 10f)]
    public float temperatureRate;


    [Range(2f, 10f)]
    public float maxTemperature;


    [Tooltip("How much the temperature will increase or decrease, the amount will flip the value for decrease")]
    [Range(.5f, 3f)]
    public float temperatureAmount;

    [Header("Balance Settings")]


    [Tooltip("What the temperature has to be between in order to trigger balance")]
    [Range(1f, 5f)]
    public float balanceRange;


    [Range(.5f, 5f)]
    public float balanceTimeRequired;

    [Tooltip("Resets time when turned back on")]
    public bool resetTimeOnActive;

    private void OnValidate()
    {
        if (balanceRange >= maxTemperature)
        {
            balanceRange = (maxTemperature/2);
        }
    }
}