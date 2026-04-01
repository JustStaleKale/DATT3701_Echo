using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    public string InteractMessage { get; }
    public void Interact();
}
public class Interactor : MonoBehaviour
{
    
    public Camera cam;
    public float interactRange = 3f;
    public LayerMask interactableLayer;
    public TextMeshProUGUI interactionText;
    public string defaultMessage = "Press 'F' to interact";
    private IInteractable currentInteractable;

    private void Update()
    {
        CheckForInteractables();
        HandleInteraction();
    }

    private void CheckForInteractables()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
        {
            IInteractable interactable = hit.collider?.GetComponent<IInteractable>();
            if (interactable != null)
            {
                currentInteractable = interactable;
                interactionText.text = interactable.InteractMessage;
            } else
            {
                currentInteractable = null;
                interactionText.text = string.Empty;
            }
        }
        
    }

    private void HandleInteraction()
    {
        if (currentInteractable != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            currentInteractable.Interact();
        }
    }
}
