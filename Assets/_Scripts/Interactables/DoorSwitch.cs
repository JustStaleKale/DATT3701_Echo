using UnityEngine;

public class DoorSwitch : MonoBehaviour, IInteractable
{

    public GameObject door;
    private Animator doorAnimator;

    void Start()
    {
        doorAnimator = door.GetComponent<Animator>();
    }

    private void OpenDoor()
    {
        doorAnimator.SetBool("character_nearby", true);
    }

    public string InteractMessage => "Press 'E' to open the door";

    public void Interact()
    {
        OpenDoor();
    }
}
    