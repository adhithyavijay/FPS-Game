using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameObject mainMenuCanvas;
    public GameObject gameUI;
    private bool isGameStarted = false;
    private bool isPaused = false;

    [Header("Game Settings")]
    [SerializeField] private float timeToWaitBeforeExit = 3f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        DetectCurrentScene();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "mainn")
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"🔄 Scene Loaded: {scene.name}");
        DetectCurrentScene();
    }

    private void DetectCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log($"🎮 GameManager Active in Scene: {currentScene}");

        if (currentScene == "Main Menu") SetupMainMenu();
        else if (currentScene == "mainn") SetupGameScene();
    }

    void SetupMainMenu()
    {
        Debug.Log("🔍 Looking for Main Menu UI...");
        mainMenuCanvas = GameObject.Find("MainMenuCanvas");

        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(true);
            Debug.Log("✅ Main Menu Canvas Activated");
        }
        else
        {
            Debug.LogWarning("❌ Main Menu Canvas NOT found in 'Main Menu' scene. Make sure it's named correctly!");
        }
    }

    void SetupGameScene()
    {
        Debug.Log("🔍 Looking for Game UI...");
        gameUI = GameObject.Find("GameUI");

        if (gameUI != null)
        {
            gameUI.SetActive(true);
            Debug.Log("✅ Game UI Activated");
        }
        else
        {
            Debug.LogWarning("❌ Game UI NOT found in 'mainn' scene. Make sure it's named correctly!");
        }
    }

    public void StartGame()
    {
        if (isGameStarted) return;
        isGameStarted = true;
        Debug.Log("🚀 Starting Game...");
        SceneManager.LoadScene("mainn"); // ✅ Loads the game scene
    }

    public void QuitGame()
    {
        Debug.Log("❌ Quit Button Clicked! Exiting Game...");
        Application.Quit();
    }

    public void PauseGame()
    {
        Debug.Log("⏸ Game Paused!");
        isPaused = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("PauseMenuScene", LoadSceneMode.Additive);
    }

    public void ResumeGame()
    {
        Debug.Log("▶ Game Resumed!");
        isPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.UnloadSceneAsync("PauseMenuScene");
    }

    public void OnPlayerDied()
    {
        Debug.Log("☠ Player Died! Returning to Main Menu...");
        Invoke(nameof(LoadMainMenu), timeToWaitBeforeExit);
    }

    private void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu"); // ✅ Loads the main menu
    }
}
