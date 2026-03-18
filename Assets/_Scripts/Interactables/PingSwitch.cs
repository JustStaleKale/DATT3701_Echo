using UnityEngine;

public class PingSwitch : MonoBehaviour
{
    public GameObject door;
    private Animator doorAnimator;

    public Light batteryLight;
    private bool hasTriggered;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorAnimator = door.GetComponent<Animator>();
        hasTriggered = false;
        //batteryLight = GetComponent<Light>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered)
        {
            batteryLight.color = Color.green;
            doorAnimator.SetBool("character_nearby", true);
            hasTriggered = true;
        }
        
    }
    
    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Echo"))
    //     {
    //         doorAnimator.SetBool("character_nearby", false);
    //     }
    // }
}
