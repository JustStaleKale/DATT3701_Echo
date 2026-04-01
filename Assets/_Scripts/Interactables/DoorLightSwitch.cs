
using UnityEngine;

public class DoorLightSwitch : MonoBehaviour
{
    public GameObject door;
    public GameObject doorLightGO;
    public GameEvent changeColour;
    public Light doorLight;
    private Renderer lightRenderer;
    private Animator doorAnimator;
    private bool hasTriggered;
    private string interactMessage = "Press 'F' to open the door";

    void Start()
    {
        doorAnimator = door.GetComponent<Animator>();
        lightRenderer = doorLightGO.GetComponent<Renderer>();
        hasTriggered = false;
        doorLight.color = Color.red;
        lightRenderer.material.SetColor("_EmissionColor", Color.red);
        
    }

    public void OpenDoor()
    {
        if (!hasTriggered)
        {
            
            doorAnimator.SetBool("character_nearby", true);
            door.gameObject.layer = LayerMask.NameToLayer("Default");
            doorLightGO.layer = LayerMask.NameToLayer("Default");
            hasTriggered = true;
            changeColour.Raise(this, null);
            interactMessage = "A door has opened!";

        }
    }

    public string InteractMessage => interactMessage;

    public void Interact()
    {
        ChangeColors();
        OpenDoor();
    }
    private void ChangeColors()
    {
        doorLight.color = Color.green;
        lightRenderer.material.SetColor("_EmissionColor", Color.green);
        Debug.Log("Door light color changed to green.");
    }
}
