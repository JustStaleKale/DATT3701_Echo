using TMPro;
using UnityEngine;

public class UI_WInState : MonoBehaviour
{

    private bool win = false;
    public float textDuration = 2f;
    public TextMeshProUGUI winText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Win(Component sender, object data)
    {
        win = (bool) data;
    }

    void Update()
    {
        if (win && textDuration > 0)
        {
            winText.text = "You Win!";
            textDuration -= Time.deltaTime;
        } else
        {
            winText.text = "";
            win = false;
            textDuration = 2f;
        }
    }
}
