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

    private Dialogue dialogue;
    private int index;


    public void StartDialogue(Dialogue newDialogue)
    {
        textbox.gameObject.SetActive(true);
        dialogue = newDialogue;
        index = 0;

        ShowLine();
    }

    void ShowLine()
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

    void EndDialogue()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            NextLine();
        }
    }
}
