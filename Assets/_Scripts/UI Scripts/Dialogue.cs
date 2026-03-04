using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI text;
    public GameObject arrow;
    public GameEvent pauseTime;
    //public GameEvent ActivatePlayerUI;
    public GameEvent StartGame;
    //public GameObject skipTutorialButton;

    public PlayerInputs playerInput;

    public string[] lines = {
        "You're finally awake. I almost lost all hope. Don't worry if you can't see, your visual processor was damaged so I had to be a little creative when putting you back together.", 
        "I jerry rigged a state of the art sonar to your vision. You should be able to see using echolocation. Press the Right Mouse Button to see your surroundings. Press E to get try it out now!"
    };
    public float textSpeed = 0.025f;
    private int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerInput = new PlayerInputs();
        text.text = string.Empty;
        //textSpeed = 0.025f;
        index = 0;

        playerInput.Player.Interact.performed += HandleClick;

    }

    // void Start()
    // {
    //     pauseTime.Raise(this, true);
    //     StartDialogue();
    // }

    private void HandleClick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if(text.text == lines[index])
            {
                NextLine();
            } else {
                StopAllCoroutines();
                text.text = lines[index];
                arrow.SetActive(true);
            }
        }
    }
    // public void SetLines(Component sender, object data)
    // {
    //     lines = (string[]) data;
    //     index = 0;
    // }

    // public void Speak(Component sender, object data)
    // {
    //     pauseTime.Raise(this, true);
    //     text.text = string.Empty;
    //     StartDialogue();
    // }

    void OnEnable()
    {
        playerInput.Player.Enable();
    }

    void OnDisable()
    {
        playerInput.Player.Disable();
    }

    public void DialogueEvent(Component sender, object data)
    {
        lines = (string[]) data;
        index = 0;
        pauseTime.Raise(this, true);
        text.text = string.Empty;
        dialoguePanel.SetActive(true);
        StartDialogue();
    }

    private void StartDialogue()
    {
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        arrow.SetActive(false);
        foreach(char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }
    
    private void NextLine()
    {
        if (index < lines.Length-1)
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        } else
        {
            text.text = string.Empty;
            //ActivatePlayerUI.Raise(this, true);
            StartGame.Raise(this, true);
            pauseTime.Raise(this, false);
            dialoguePanel.SetActive(false);
        }
    }

}
