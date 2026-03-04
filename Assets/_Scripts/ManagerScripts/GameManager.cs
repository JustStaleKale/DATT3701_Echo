using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameEvent StartGame;
    private float time;
    private bool isRunning;

    void Start()
    {
        time = 0;
        Time.timeScale = 1;
        isRunning = true;
    }

    // void Update()
    // {
    //     if(isRunning)
    //     {
    //         time += Time.deltaTime;
    //         if(time > 2 && time < 3)
    //         {
    //             //Time.timeScale = 0;
    //             //StartGame.Raise(this, true);
    //             time = 5;
    //         }
    //     }
        
    // }

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
