using UnityEngine;
using UnityEngine.UIElements;

public class TutorialManager : MonoBehaviour
{
    public GameEvent StartDialogue;
    public Dialogue shootDialogue;
    public Dialogue pingDialogue;
    private bool hasShot;
    private bool hasPinged;
    private bool canRun;
    
    private void Start()
    {
        hasShot = false;
        hasPinged = false;
        canRun = false;
    }

    public void OnShootPing(Component sender, object data)
    {
        if (!hasShot && canRun)
        {
            hasShot = true;
            Invoke(nameof(ShotPing), 1f);
        }
    }

    public void OnPinged(Component sender, object data)
    {
        if (hasShot && !hasPinged && canRun)
        {
            Invoke(nameof(Pinged), 1f);
            hasPinged = true;
        }
        
    }

    public void StartTutorial(Component sender, object data)
    {
        canRun = true;
    }

    private void ShotPing()
    {
        StartDialogue.Raise(this, shootDialogue);
    }

    private void Pinged()
    {
        StartDialogue.Raise(this, pingDialogue);
    }
}
