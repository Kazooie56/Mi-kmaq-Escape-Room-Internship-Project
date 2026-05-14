using UnityEngine;

public class TemperatureSystem : MonoBehaviour
{
    [SerializeField] private TemperaturePuzzleSetting setting;
    [SerializeField] private TemperatureLights temperatureLights;
    [SerializeField] private bool canDebug;
    private TemperatureMode currentMode;
    [SerializeField]private float currentTemperature;
    private float temperatureDirection;
    [SerializeField] private float balanceTimer;
    private bool isOn;
    private bool isSolved;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetMode(TemperatureMode.Off);
        temperatureLights = GetComponent<TemperatureLights>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (isSolved) return;

        HandleTemperatureMode();

        if (!canDebug) return;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetMode(TemperatureMode.Cold);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetMode(TemperatureMode.Off);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetMode(TemperatureMode.Hot);
        }
    }
    public void SetMode(TemperatureMode newMode)
    {
        if (currentMode == newMode) return;

        if (setting.resetTimeOnActive && currentMode == TemperatureMode.Off)
        {
            ResetBalanceTimer();
        }

        currentMode = newMode;


        //What to do when switching states
        switch (currentMode)
        {
            case TemperatureMode.Hot:
                SetTemperatureDirection(setting.temperatureAmount);
                isOn = true;
                break;
            case TemperatureMode.Off:
                isOn = false;
                break;
            case TemperatureMode.Cold:
                SetTemperatureDirection(-setting.temperatureAmount);
                isOn = true;
                break;
        }
    }

    private void HandleTemperatureMode()
    {
        if (setting == null)
        {
            Debug.Log($"Settings is null please add setting object");
            return;
        }
        if (!isOn) return;
        ChangeTemperature();
        CheckBalance();
        if (temperatureLights != null) temperatureLights.SetTemperatureLights(currentTemperature);
    }
    private void ChangeTemperature()
    {
        currentTemperature += temperatureDirection * setting.temperatureRate * Time.deltaTime;
        currentTemperature = Mathf.Clamp(currentTemperature, -setting.maxTemperature, setting.maxTemperature);
    }
    private void CheckBalance()
    {
        if (IsBalanced())
        {
            balanceTimer += Time.deltaTime;

            if (balanceTimer >= setting.balanceTimeRequired)
            {
                SetMode(TemperatureMode.Off);
                isSolved = true;
            }
        }
        else
        {
            ResetBalanceTimer();
        }
    }

    private void ResetBalanceTimer()
    {
        balanceTimer = 0;
    }

    private bool IsBalanced()
    {
        return Mathf.Abs(currentTemperature) <= setting.balanceRange;
    }

    private void SetTemperatureDirection(float temperature)
    {
        temperatureDirection = temperature;
    }
}
