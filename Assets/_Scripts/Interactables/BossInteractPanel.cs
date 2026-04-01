using UnityEngine;

public class BossInteractPanel : MonoBehaviour, IInteractable
{
    public ItemCount itemsCollected;
    public GameEvent triggerWin;
    public GameEvent startGame;
    public GameEvent StartDialogue;
    public Dialogue failDialogue;
    public Dialogue winDialogue;
    private string interactMessage = "Press 'F' to deactivate the Overseer";

    public string InteractMessage => interactMessage;

    public void Interact()
    {
        if (itemsCollected.count >= 4)
        {
            triggerWin.Raise(this, null);
            StartDialogue.Raise(this, winDialogue);
            interactMessage = "You Win! The Overseer has been deactivated.";
        } else
        {
            //startGame.Raise(this, null);
            triggerWin.Raise(this, null);
            StartDialogue.Raise(this, failDialogue);    
            interactMessage = "You win, but at a cost. The Overseer has self-destructed.";
        }
    }

}
