using UnityEngine;

public class DoorSwitch : MonoBehaviour, IInteractable
{

    public GameObject door;
    private Animator doorAnimator;
    private string interactMessage = "Press 'E' to open the door";

    void Start()
    {
        doorAnimator = door.GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        doorAnimator.SetBool("character_nearby", true);
    }

    public string InteractMessage => interactMessage;

    public void Interact()
    {
        OpenDoor();
        interactMessage = string.Empty;
    }
}
    