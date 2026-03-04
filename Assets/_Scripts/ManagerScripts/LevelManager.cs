using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    //public GameEvent setScene;
    public Canvas loadCanvas;
    public Image fillBar;
    //public Image panelImage;
    private string sceneName;
    //private bool isLevel2Complete = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //setScene.Raise(this, SceneManager.GetActiveScene().name);
    }
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    private async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        
        loadCanvas.enabled = true;

        do
        {
            await Task.Delay(100);
            fillBar.fillAmount = scene.progress / 0.9f;
        }while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        await Task.Delay(1000);
        loadCanvas.enabled = false;
        //setScene.Raise(this, SceneManager.GetActiveScene().name);
    } 

    public async void LoadScene(Component sender, object data)
    {
        var scene = SceneManager.LoadSceneAsync((string) data);
        scene.allowSceneActivation = false;
        
        loadCanvas.enabled = true;

        do
        {
            await Task.Delay(100);
            fillBar.fillAmount = scene.progress / 0.9f;

        }while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        await Task.Delay(1000);
        loadCanvas.enabled = false;
        //setScene.Raise(this, SceneManager.GetActiveScene().name);
    } 

    public void NextScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        
        if(sceneName == "Main Menu")
        {
            LoadScene("TutorialLevel");

        } else if (sceneName == "TutorialLevel")
        {
            LoadScene("Level 1");

        } 

        if (sceneName == "Main Menu")
        {
            LoadScene("TutorialLevel");
        }
         else
        {
            LoadScene("Level 1");
        }
    }

    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    // public void EditorNextScene(Component sender, object data)
    // {
    //     EditorSceneManager.SaveOpenScenes();
    //     EditorSceneManager.OpenScene((string) data, OpenSceneMode.Single);
         
    // }

    public void RestartScene(Component sender, object data)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        LoadScene(currentScene.name);
    }
}
