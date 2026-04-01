using UnityEngine;

public class PingSwitch : MonoBehaviour
{
    public GameObject door;
    private Animator doorAnimator;

    public Light batteryLight;

    public GameObject doorLightGO;
    public Light doorLight;
    private Renderer lightRenderer;
    private bool hasTriggered;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorAnimator = door.GetComponent<Animator>();
        hasTriggered = false;
        batteryLight.color = Color.red;
        doorLight.color = Color.red;
        lightRenderer = doorLightGO.GetComponent<Renderer>();
        lightRenderer.material.SetColor("_EmissionColor", Color.red);
        door.gameObject.layer = LayerMask.NameToLayer("Invisible");
        doorLightGO.layer = LayerMask.NameToLayer("Invisible");
        //batteryLight = GetComponent<Light>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered)
        {
            batteryLight.color = Color.green;
            doorLight.color = Color.green;
            lightRenderer.material.SetColor("_EmissionColor", Color.green);
            doorAnimator.SetBool("character_nearby", true);
            door.gameObject.layer = LayerMask.NameToLayer("Default");
            doorLightGO.layer = LayerMask.NameToLayer("Default");
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
