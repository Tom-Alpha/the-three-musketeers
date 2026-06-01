using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Panels")] public GameObject mainMenuPanel;
    public GameObject settingsPanel;

    // Called when Play is pressed
    public void PlayGame()
    {
        SceneManager.LoadScene("BookScene");
    }

    // Open settings menu
    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    // Back to main menu
    public void BackToMainMenu()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // Quit game
    public void QuitGame()
    {
        Debug.Log("Game Closed");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
    