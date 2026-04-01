using UnityEngine;

public class MainMenu_Start : MonoBehaviour
{
    public GameEvent nextLevel;
    public GameEvent quitLevel;
    public ItemCount itemCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemCount.count = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        nextLevel.Raise(this, null);
    }

    public void QuitGame()
    {
        quitLevel.Raise(this, null);
    }
}
