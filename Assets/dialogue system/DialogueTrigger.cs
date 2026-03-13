using UnityEngine;

public class DialogueTrigger :MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManagerNEW manager;
    public GameObject dialogueUI;
    private Collider trigger;

    void Start()
    {
        //TriggerDialogue();
        trigger = GetComponent<Collider>();
    }

    public void TriggerDialogue(Component sender, object data)
    {
        dialogueUI.SetActive(true);
        manager.StartDialogue((Dialogue) data);
    }

    private void TriggerDialogue()
    {
        dialogueUI.SetActive(true);
        manager.StartDialogue(dialogue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TriggerDialogue();
            gameObject.SetActive(false);
        }
    }

}
