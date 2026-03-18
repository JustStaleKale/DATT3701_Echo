using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameEvent StartDialogue;
    public Dialogue dialogue;
    private float time;
    private bool isRunning;

    void Awake()
    {
        time = 0;
        Time.timeScale = 1;
        isRunning = true;
    }

    void Update()
    {
        if(isRunning)
        {
            time += Time.deltaTime;
            if(time > 1 && time < 2)
            {
                //Time.timeScale = 0;
                StartDialogue.Raise(this, dialogue);
                time = 5;
                isRunning = false;
            }
        }
        
    }

    public void PauseGame (Component sender, object data)
    {
        if ((bool) data)
        {
            Time.timeScale = 0;

        } else
        {
            Time.timeScale = 1;
        }
    }
}
