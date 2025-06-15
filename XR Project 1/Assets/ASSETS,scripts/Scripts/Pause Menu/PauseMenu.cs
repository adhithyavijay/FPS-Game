using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void ResumeGame()
    {
        if (GameManager.instance != null)
        {
            Debug.Log("▶ Resume Button Clicked! Resuming Game...");
            GameManager.instance.ResumeGame();
        }
        else
        {
            Debug.LogError("❌ GameManager instance is missing!");
        }
    }

    public void GoToMainMenu()
    {
        Debug.Log("🏠 Main Menu Button Clicked! Returning to Main Menu...");
        Time.timeScale = 1f; // ✅ Ensure time resets before loading main menu
        SceneManager.LoadScene("Main Menu"); // ✅ Load Main Menu scene
    }

    public void QuitGame()
    {
        Debug.Log("❌ Quit Button Clicked! Exiting Game...");
        Application.Quit();
    }
}
