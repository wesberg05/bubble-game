using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject gameOverScreen; // The Game Over screen UI
    

    [Header("Buttons")]
    public GameObject restartButton;  // Restart button on the Game Over screen
    public GameObject mainMenuButton; // Main Menu button on the Game Over screen

    private void Start()
    {
        // Hide the Game Over screen initially
        gameOverScreen.SetActive(false);

        // Set up button listeners
        restartButton.SetActive(true);
        mainMenuButton.SetActive(true);

        restartButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(RestartGame);
        mainMenuButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(GoToMainMenu);
    }

    public void ShowGameOverScreen()
    {
        Debug.Log("Game Over Triggered in GameManager!");
        gameOverScreen.SetActive(true);  // Show the Game Over screen
      
    }

    public void RestartGame()
    {
        Debug.Log("Restarting Game from GameManager!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
        gameOverScreen.SetActive(false); // Hide the Game Over screen on restart
    }

    public void GoToMainMenu()
    {
        Debug.Log("Going back to Main Menu from GameManager!");
        SceneManager.LoadScene("Title"); // Assuming "Title" is the name of your title screen scene
        gameOverScreen.SetActive(false); // Hide the Game Over screen when going to the main menu
    }
}
