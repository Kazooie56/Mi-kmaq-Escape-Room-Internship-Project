using UnityEngine;

public class TemperatureButton : MonoBehaviour, IInteractable
{
    [SerializeField] private TemperatureSystem temperatureSystem;
    [SerializeField] private TemperatureMode temperatureMode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        temperatureSystem ??= GetComponentInParent<TemperatureSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnInteract(GameObject interactor)
    {
        temperatureSystem.SetMode(temperatureMode);
    }
}
