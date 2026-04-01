using UnityEngine;

public class ServerSwitch : MonoBehaviour
{

    public GameObject doorLightGO;
    public Light doorLight;
    public BatteryCount pinged;
    private bool hasTriggered;
    private Renderer lightRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lightRenderer = doorLightGO.GetComponent<Renderer>();
        hasTriggered = false;
        doorLight.color = Color.red;
        lightRenderer.material.SetColor("_EmissionColor", Color.red);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered)
        {
            doorLight.color = Color.green;
            lightRenderer.material.SetColor("_EmissionColor", Color.green);
            doorLightGO.layer = LayerMask.NameToLayer("Default");
            pinged.count++;
            hasTriggered = true;
            gameObject.SetActive(false);
        }
    }
}
