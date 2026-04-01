using UnityEngine;

public class ItemInteractable : MonoBehaviour, IInteractable
{
    public ItemCount itemsCollected;
    public Item item;
    public GameEvent StartDialogueEvent;
    private string interactMessage;
    private string itemName;
    private Dialogue dialogue;
    private bool isCollected = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isCollected = false;
        itemName = item.itemName;
        dialogue = item.dialogue;
        interactMessage = "Press 'F' to pick up " + itemName;
    }

    public string InteractMessage => interactMessage;

    public void Interact()
    {
        if (!isCollected)
        {
            itemsCollected.count++;
            //Debug.Log($"Picked up {itemName}. Total items collected: {itemsCollected.count}");
            StartDialogueEvent.Raise(this, dialogue);
            isCollected = true;
            gameObject.SetActive(false);    
        }
        
    }
}
