using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private Image[] heartImages;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHeartUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // Prevent negative health
        UpdateHeartUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHeartUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHealth)
            {
                heartImages[i].sprite = fullHeart;
            }
            else
            {
                heartImages[i].sprite = emptyHeart;
            }
        }
    }

    private void Die()
    {
        // Disable the witch game object
        gameObject.SetActive(false);
        
        // You might want to add game over logic here
        Debug.Log("Witch has died!");
    }
}
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private float heartSpacing = 0.5f;
    [SerializeField] private Vector2 heartsOffset = new Vector2(0, -0.5f); // Offset below the witch

    private int maxHealth = 3;
    private int currentHealth;
    private Image[] heartImages;
    private Canvas worldSpaceCanvas;
    private RectTransform heartsPanel;

    private void Start()
    {
        currentHealth = maxHealth;
        SetupHealthUI();
        UpdateHeartUI();
    }

    private void SetupHealthUI()
    {
        // Create a world space canvas as a child of the witch
        GameObject canvasObj = new GameObject("HealthCanvas");
        canvasObj.transform.SetParent(transform);
        canvasObj.transform.localPosition = Vector3.zero;
        
        worldSpaceCanvas = canvasObj.AddComponent<Canvas>();
        worldSpaceCanvas.renderMode = RenderMode.WorldSpace;
        worldSpaceCanvas.sortingOrder = 1;
        
        // Add a canvas scaler
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.dynamicPixelsPerUnit = 100f;

        // Create a panel for hearts
        GameObject panelObj = new GameObject("HeartsPanel");
        panelObj.transform.SetParent(canvasObj.transform);
        heartsPanel = panelObj.AddComponent<RectTransform>();
        heartsPanel.localPosition = new Vector3(heartsOffset.x, heartsOffset.y, 0);
        heartsPanel.localScale = Vector3.one * 0.01f; // Scale down the UI to match world space

        // Create hearts
        heartImages = new Image[maxHealth];
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject heartObj = Instantiate(heartPrefab, panelObj.transform);
            RectTransform heartTransform = heartObj.GetComponent<RectTransform>();
            heartTransform.anchoredPosition = new Vector2(i * heartSpacing * 100, 0);
            heartImages[i] = heartObj.GetComponent<Image>();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateHeartUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHeartUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].sprite = i < currentHealth ? fullHeart : emptyHeart;
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        Debug.Log("Witch has died!");
    }
}
