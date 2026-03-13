using UnityEngine;

public class DialogueTrigger :MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManagerNEW manager;

    void Start()
    {
        manager.StartDialogue(dialogue);
    }
}
