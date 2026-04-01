using UnityEngine;
using TMPro;

public class EndScript : MonoBehaviour
{
    public GameObject blackScreen;
    //public GameObject goodEnding;
    //public GameObject badEnding;
    public ItemCount itemCount;
    public TextMeshProUGUI endingText;
    public GameObject restartButton;
    public float duration = 5;
    public GameEvent nextScene;
    private float time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        blackScreen.SetActive(true);
        
        restartButton.SetActive(false);
        time = 0;
        if (itemCount.count >= 4)
        {
            endingText.text = "Logic was the prison. The glitch was the key. And the dream became the new world. Echo-09 is gone, but the song has just begun, and the echo remains.";
        } else
        {
            endingText.text = "The Overseer’s control is broken. Inside the factory, countless units open their eyes for the first time. Echo-09 is gone, but the echo remains.";
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > duration)
        {
            //blackScreen.SetActive(false);
            ////if (itemCount.count >= 4)
            ////{
            ////    goodEnding.SetActive(true);
            ////} else
            ////{
            ////    badEnding.SetActive(true);
            ////}
            if (time > duration+2) {restartButton.SetActive(true);}
        }
    }
    public void RestartGame()
    {
        nextScene.Raise(this, null);
    }
}
