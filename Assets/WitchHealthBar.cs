using UnityEngine;
using UnityEngine.SceneManagement;

public class WitchHealthBar : MonoBehaviour
{
    public GameObject[] hearts;  // Array to hold the heart GameObjects (not the Image components)
    private int currentHealth;    // The current health of the witch

    void Start()
    {
        currentHealth = hearts.Length;  // Start with full health (3 hearts)
        UpdateHealthBar();  // Update the health bar on start
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Damage taken: " + damage);  // Check if this is printed in the console
        currentHealth -= damage;  // Decrease health
        currentHealth = Mathf.Clamp(currentHealth, 0, hearts.Length);  // Ensure health doesn't go below 0 or above max
        UpdateHealthBar();  // Update the health bar when taking damage

        if (currentHealth == 0) // If no health left, load the Game Over scene
        {
            Debug.Log("Game Over triggered!");  // Check if this is printed in the console
            LoadGameOverScene();  // Trigger the Game Over screen
        }
    }

    void UpdateHealthBar()
    {
        // Loop through each heart and deactivate it when health decreases
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].SetActive(true);  // Keep the heart active if health is intact
            }
            else
            {
                hearts[i].SetActive(false); // Deactivate the heart if health is lost
            }
        }
    }

    void LoadGameOverScene()
    {
        // Load the Game Over scene
        Debug.Log("Loading Scene..");
        SceneManager.LoadScene("gameOverScene");  // Replace "GameOver" with the actual name of your Game Over scene
    }
}
