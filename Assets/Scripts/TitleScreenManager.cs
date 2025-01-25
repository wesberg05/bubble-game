using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject titleScreen; // The title screen UI
    public GameObject settingsMenu; // The settings menu UI

    [Header("Buttons")]
    public Button startButton; // The "Start" button
    public Button settingsButton; // The "Settings" button
    public Button backButton; // The "Back" button in the settings menu

    [Header("Game Scene")]
    public string gameSceneName = "SampleScene"; // Name of the scene to load for the game

    private void Start()
    {
        // Ensure the correct initial UI state
        titleScreen.SetActive(true);
        settingsMenu.SetActive(false);

        // Attach button click events
        startButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);
        backButton.onClick.AddListener(CloseSettings);
    }

    public void StartGame()
    {
        // Log to confirm StartGame is being called
        Debug.Log("StartGame method triggered!");
        // Load the game scene
        settingsMenu.SetActive(false);
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenSettings()
    {
        // Log to confirm OpenSettings is being called
        Debug.Log("OpenSettings method triggered!");
        // Show the settings menu and hide the title screen
        settingsMenu.SetActive(true);
        titleScreen.SetActive(false);
    }

    public void CloseSettings()
    {
        // Log to confirm CloseSettings is being called
        Debug.Log("CloseSettings method triggered!");
        // Hide the settings menu and show the title screen
        settingsMenu.SetActive(false);
        titleScreen.SetActive(true);
    }
}
