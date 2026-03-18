using UnityEngine;

public class CountBattery : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public BatteryCount batteryCount;

    private void Start()
    {
        batteryCount.count = 0;
    }
    
}
