using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossGameManager : MonoBehaviour
{
    public GameEvent StartDialogue;
    public Dialogue dialogue;
    private float time;
    private bool isRunning;
    private bool hasWon;
    public BatteryCount pings;
    public ItemCount itemsCollected;
    public Dialogue winDialogue;
    public GameEvent triggerWin;
    public List<GameObject> BadEnding;

    void Awake()
    {
        hasWon = false;
        pings.count = 0;
        time = 0;
        Time.timeScale = 1;
        isRunning = true;
        foreach (GameObject obj in BadEnding)
        {
            obj.SetActive(false);
        }
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
                //StartGame();
                time = 5;
                isRunning = false;
            }
        }

        if (pings.count >= 20 && !hasWon)
        {
            // Trigger win condition
            triggerWin.Raise(this, null);
            StartDialogue.Raise(this, winDialogue);
            hasWon = true;
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

    public void StartGame()
    {
        if (itemsCollected.count < 4)
        {
            foreach (GameObject obj in BadEnding)
            {
                obj.SetActive(true);
            }
        }
    }
}
