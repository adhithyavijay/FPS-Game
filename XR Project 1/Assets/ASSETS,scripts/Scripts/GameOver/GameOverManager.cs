using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // ✅ Unlock cursor for menu navigation
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Debug.Log("🔄 Restarting Game...");
        SceneManager.LoadScene("mainn"); // ✅ Ensure this is your actual game scene name
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("🏠 Returning to Main Menu...");
        SceneManager.LoadScene("Main Menu"); // ✅ Ensure this is your actual main menu scene name
    }
}
