using System.Collections.Generic;
using UnityEngine;
public class TemperatureLights : MonoBehaviour
{
    [SerializeField] private TemperaturePuzzleSetting setting;
    [SerializeField] private bool getActiveChildren = true;
    [SerializeField] private LightingConfig coldLighting;
    [SerializeField] private LightingConfig balanceLighting;
    [SerializeField] private LightingConfig hotLighting;

    private List<Light> lights = new List<Light>();
    private float currentTemperature;

    void Start()
    {
        GetAllLights();
        UpdateLighting();
    }

    public void SetTemperatureLights(float newTemperature)
    {
        currentTemperature = newTemperature;
        UpdateLighting();
    }

    private void UpdateLighting()
    {
        float progress = GetTemperatureProgress();

        // Gets target lighting. if current temp is greater than 0 give hot lighting else give cold
        LightingConfig target = (currentTemperature > 0) ? hotLighting : coldLighting;

        foreach (Light light in lights)
        {
            light.color = Color.Lerp(balanceLighting.colorLight, target.colorLight, progress);
            light.intensity = Mathf.Lerp(balanceLighting.intensity, target.intensity, progress);
            light.range = Mathf.Lerp(balanceLighting.range, target.range, progress);
            light.spotAngle = Mathf.Lerp(balanceLighting.spotAngle, target.spotAngle, progress);
            light.innerSpotAngle = Mathf.Lerp(balanceLighting.innerSpotAngle, target.innerSpotAngle, progress);
        }
    }

    private float GetTemperatureProgress()
    {
        // Returns 0 to 1 based of the current temp, 0 = balance 1 = cold/hot
        return Mathf.InverseLerp(setting.balanceRange, setting.maxTemperature, Mathf.Abs(currentTemperature));
    }
    /// <summary>
    /// Creates a new list of lights from children and turns off the Color Temperature on the light
    /// </summary>
    private void GetAllLights()
    {
        // Gets a new list of lights from children, a bool thats checks for activeChildren
        lights = new List<Light>(GetComponentsInChildren<Light>(getActiveChildren));

        foreach (Light light in lights)
        {
            if (light.useColorTemperature == true) light.useColorTemperature = false;
        }
    }
}
