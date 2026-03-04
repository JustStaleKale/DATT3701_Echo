using UnityEngine;

public class DoorSwitch : MonoBehaviour, IInteractable
{

    public GameObject door;
    private Animator doorAnimator;
<<<<<<< Updated upstream
    private string interactMessage = "Press 'E' to open the door";
=======
>>>>>>> Stashed changes

    void Start()
    {
        doorAnimator = door.GetComponent<Animator>();
    }

<<<<<<< Updated upstream
    public void OpenDoor()
=======
    private void OpenDoor()
>>>>>>> Stashed changes
    {
        doorAnimator.SetBool("character_nearby", true);
    }

<<<<<<< Updated upstream
    public string InteractMessage => interactMessage;
=======
    public string InteractMessage => "Press 'E' to open the door";
>>>>>>> Stashed changes

    public void Interact()
    {
        OpenDoor();
<<<<<<< Updated upstream
        interactMessage = string.Empty;
=======
>>>>>>> Stashed changes
    }
}
    