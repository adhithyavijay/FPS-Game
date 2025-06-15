using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_InputField playerNameInput;
    [SerializeField] TMP_Text playerNameHolder;

    void Start()
    {
        LoadPlayerName();
    }

    public void SetPlayerName()
    {
        string playerName = playerNameInput.text;
        PlayerPrefs.SetString("PlayerName", playerName); // ✅ Save name
        PlayerPrefs.Save();

        playerNameHolder.text = playerName;
        Debug.Log("Player name saved: " + playerName);
    }

    void LoadPlayerName()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            string savedName = PlayerPrefs.GetString("PlayerName");
            playerNameHolder.text = savedName;
            playerNameInput.text = savedName;
            Debug.Log("Loaded player name: " + savedName);
        }
    }
}
