using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    public Sprite portrait;

    [TextArea(3, 5)]
    public string text;
}