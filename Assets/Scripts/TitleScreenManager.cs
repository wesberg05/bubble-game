using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject titleScreen;      // The title screen UI
    public GameObject settingsMenu;     // The settings menu UI

    [Header("Buttons")]
    public Button startButton;          // The "Start" button
    public Button settingsButton;       // The "Settings" button
    public Button backButton;           // The "Back" button in the settings menu

    [Header("Scene Names")]
    public string gameSceneName = "GameScene"; // Name of the scene to load for the game
    public string mainMenuSceneName = "Title"; // Name of the main menu scene

    private void Start()
    {
        // Set initial UI states
        titleScreen.SetActive(true);
        settingsMenu.SetActive(false);

        // Attach button click events
        startButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);
        backButton.onClick.AddListener(CloseSettings);
    }

    public void StartGame()
    {
        Debug.Log("Game Started!");
        settingsMenu.SetActive(false);
        titleScreen.SetActive(false); // Hide title screen when starting the game
        SceneManager.LoadScene(gameSceneName); // Load the GameScene
    }

    public void OpenSettings()
    {
        Debug.Log("Settings Opened!");
        settingsMenu.SetActive(true);
        titleScreen.SetActive(false);
    }

    public void CloseSettings()
    {
        Debug.Log("Settings Closed!");
        settingsMenu.SetActive(false);
        titleScreen.SetActive(true);
    }
}
