using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Dynamic;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    //public GameEvent setScene;
    public GameObject loadCanvas;
    public Slider slider;
    private CanvasGroup loadCanvasGroup;
    private float fadeDuration = 1f;
    //public Image panelImage;
    private string sceneName;
    //private bool isLevel2Complete = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     //setScene.Raise(this, SceneManager.GetActiveScene().name);
    // }
    
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

        loadCanvasGroup = loadCanvas?.GetComponent<CanvasGroup>();
    }
    private IEnumerator FadeIn(float start, float end)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            loadCanvasGroup.alpha = Mathf.Lerp(start, end, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        loadCanvasGroup.alpha = end;
    }

    private async Task LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        
        loadCanvas.SetActive(true);
        StartCoroutine(FadeIn(0f, 1f));
        await Task.Delay(1000);
        // panel.GetComponent<Image>().CrossFadeAlpha(0.99f, 0.5f, true);
        //await Task.Delay(1000);

        do
        {
            await Task.Delay(100);
            slider.value = scene.progress / 0.9f;
        }while (scene.progress < 0.9f);

        
        scene.allowSceneActivation = true;
        StartCoroutine(FadeIn(1f, 0f));
        await Task.Delay(1000);
        //await Task.Delay(1000);
        
        // panel.GetComponent<Image>().CrossFadeAlpha(0.01f, 0.5f, true);
        loadCanvas.SetActive(false);
        //setScene.Raise(this, SceneManager.GetActiveScene().name);
    } 

    public async void LoadScene(Component sender, object data)
    {
        await LoadScene((string) data);
    } 

    public async void NextScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        
        if(sceneName == "Main Menu")
        {
            await LoadScene("TutorialLevel");

        } else if (sceneName == "TutorialLevel")
        {
            await LoadScene("Second Scene");

        } else if (sceneName == "Second Scene")
        {
            await LoadScene("Third Scene");
        } else if (sceneName == "Third Scene")
        {
            await LoadScene("Laser hallway");
        } else if (sceneName == "Laser hallway")
        {
            await LoadScene("Level 1");
        } else if (sceneName == "Level 1")
        {
            await LoadScene("Main Menu");
        } 

        // if (sceneName == "Main Menu")
        // {
        //     LoadScene("TutorialLevel");
        // }
        //  else
        // {
        //     LoadScene("Level 1");
        // }
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

    public async void RestartScene(Component sender, object data)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        await LoadScene(currentScene.name);
    }

    public void WinState(Component sender, object data)
    {
        //Debug.Log("Win State Reached");
        Invoke(nameof(NextScene), 1f);
    }
}
