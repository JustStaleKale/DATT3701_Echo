using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogueManagerNEW : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image portrait;
    public Image textbox;
    public GameEvent pauseTime;

    private Dialogue dialogue;
    private int index;
    private bool isDialogueActive;

    private void Start()
    {
        index = 0;
        isDialogueActive = false;
    }

    public void StartDialogue(Dialogue newDialogue)
    {
        pauseTime.Raise(this, true);
        textbox.gameObject.SetActive(true);
        dialogue = newDialogue;
        index = 0;
        isDialogueActive = true;

        ShowLine();
    }

    public void StartDialogue(Component sender, object data)
    {
        StartDialogue((Dialogue) data);
    }

    private void ShowLine()
    {
        DialogueLine line = dialogue.lines[index];

        nameText.text = line.speakerName;
        dialogueText.text = line.text;

        if (line.portrait != null)
        {   
            portrait.gameObject.SetActive(true);
            portrait.sprite = line.portrait;
            portrait.enabled = true;
        }
        else
        {
            portrait.enabled = false;
        }
    }

    public void NextLine()
    {
        index++;

        if (index >= dialogue.lines.Length)
        {
            EndDialogue();
            return;
        }

        ShowLine();
    }

    private void EndDialogue()
    {
        textbox.gameObject.SetActive(false);
        portrait.gameObject.SetActive(false);
        nameText.text = string.Empty;
        dialogueText.text = string.Empty;
        pauseTime.Raise(this, false);
        isDialogueActive = false;
    }

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && isDialogueActive)
        {
            NextLine();
        }
    }
}
