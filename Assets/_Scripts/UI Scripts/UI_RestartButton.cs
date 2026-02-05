using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_RestartButton : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
