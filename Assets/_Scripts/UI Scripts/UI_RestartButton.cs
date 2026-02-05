using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UI_RestartButton : MonoBehaviour
{

    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }
    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
