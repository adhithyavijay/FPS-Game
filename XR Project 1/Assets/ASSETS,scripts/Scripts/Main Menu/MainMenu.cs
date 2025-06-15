using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // ✅ Unlocks cursor for UI
        Cursor.visible = true; // ✅ Makes cursor visible
    }

    public void Play()
    {
        Debug.Log("✅ Start Button Clicked! Loading Game...");

        string sceneName = "mainn"; // ✅ Ensure this matches your actual game scene name
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.Log($"✅ Loading Scene: {sceneName}");
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"❌ Scene '{sceneName}' not found! Check Build Settings.");
        }
    }

    public void Exit()
    {
        Debug.Log("❌ Quit Button Clicked! Exiting Game...");
        Application.Quit();
    }
}
