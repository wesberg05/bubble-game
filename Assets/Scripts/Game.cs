using Unity.VisualScripting;
using UnityEngine;

//[System.Serializable]
public class Game : MonoBehaviour
{

    //init scripts and game objects
    public Ingredient[] ingredients;
    public GameObject cauldron;
    public GameObject player;
    public GameObject bubblePrefab; // Assign in inspector
    private Cauldron cauldronScript;
    private AudioManager audioManager;



    //init positions

    private Vector3 cauldronPosition;
    private Vector3 mousePosition;
    private Vector3[] ingredientPositions;
    
    //init drag/drop 
    private Ingredient draggedIngredient = null;
    private bool isDragging = false;

    [SerializeField]
    private float cooldown = 0f;
    private float logTimer = 0f;
    private const float LOG_INTERVAL = 0.2f;
    private const float COOLDOWN_DURATION = 1f;


    private void Start()
    {
        if (cauldron == null)
        {
            Debug.LogError("Cauldron GameObject not assigned!");
            return;
        }

        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in scene!");
        }


        cauldronPosition = cauldron.transform.position;
        cauldronScript = cauldron.GetComponent<Cauldron>();
        
        if (cauldronScript == null)
        {
            Debug.LogError("Cauldron script component not found on cauldron GameObject!");
        }



        ingredientPositions = new Vector3[ingredients.Length];
        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredientPositions[i] = ingredients[i].transform.position;
        }
    }

    private void Update()
    {
        // Update cooldown
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            
            // Log cooldown timer every LOG_INTERVAL seconds
            logTimer -= Time.deltaTime;
            if (logTimer <= 0)
            {
                Debug.Log($"Cooldown: {cooldown:F1} seconds");
                logTimer = LOG_INTERVAL;
            }

            // When cooldown reaches 0, spawn bubble and return ingredient
            if (cooldown <= 0)
            {
                if (cauldronScript != null && cauldronScript.HasIngredient())
                {
                    // Spawn bubble
                    if (bubblePrefab != null)
                    {
                        Vector3 spawnPos = cauldronPosition + new Vector3(Random.Range(-0.5f, 0.5f), 0.5f, 0);
                        GameObject bubbleObj = Instantiate(bubblePrefab, spawnPos, Quaternion.identity);
                        Bubble bubble = bubbleObj.GetComponent<Bubble>();
                        if (bubble != null)
                        {
                            bubble.type = cauldronScript.currentIngredient.type;
                        }
                    }
                    
                    // Return ingredient to original position
                    Ingredient expiredIngredient = cauldronScript.RemoveCurrentIngredient();
                    int index = System.Array.IndexOf(ingredients, expiredIngredient);
                    if (index != -1)
                    {
                        expiredIngredient.transform.position = ingredientPositions[index];
                    }
                }
                cooldown = 0; // Ensure cooldown stays at 0 after expiring
            }


        }



        // Convert mouse position from screen space to world space

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure the z-coordinate matches your 2D plane

        //when you click the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < ingredients.Length; i++)
            {
                if (ingredients[i].GetComponent<BoxCollider2D>().OverlapPoint(mousePosition))
                {
                    isDragging = true;
                    draggedIngredient = ingredients[i];
                    break;
                }
            }
        }
        
        //follow the mouse with picked up
        if (isDragging && draggedIngredient != null)
        {
            draggedIngredient.transform.position = mousePosition;
        }

        //check when the mouse button is released
        if (Input.GetMouseButtonUp(0) && draggedIngredient != null)
        {
            isDragging = false;
            
            // Check if the ingredient should snap to cauldron
            if (IsValidDropPosition(draggedIngredient.transform.position))
            {
                if (cauldronScript != null)
                {
                    if (cauldronScript.HasIngredient() && cooldown > 0)
                    {
                        // Return previous ingredient to its original position
                        Ingredient previousIngredient = cauldronScript.RemoveCurrentIngredient();
                        if (previousIngredient != null)
                        {
                            int prevIndex = System.Array.IndexOf(ingredients, previousIngredient);
                            if (prevIndex != -1)
                            {
                                previousIngredient.transform.position = ingredientPositions[prevIndex];
                            }
                        }
                        
                        // Place new ingredient in cauldron
                        draggedIngredient.transform.position = cauldronPosition;
                        cauldronScript.AddIngredient(draggedIngredient);
                        cooldown = COOLDOWN_DURATION;
                        audioManager.PlayBoilSound();

                    }
                    else if (!cauldronScript.HasIngredient())
                    {
                        // First ingredient - move it to the cauldron
                        draggedIngredient.transform.position = cauldronPosition;
                        cauldronScript.AddIngredient(draggedIngredient);
                        cooldown = COOLDOWN_DURATION;
                        audioManager.PlayBoilSound();

                    }
                    else
                    {
                        // Return dragged ingredient to its starting position if cooldown is 0
                        int draggedIndex = System.Array.IndexOf(ingredients, draggedIngredient);
                        if (draggedIndex != -1)
                        {
                            draggedIngredient.transform.position = ingredientPositions[draggedIndex];
                        }
                    }
                }



                else
                {
                    Debug.LogError("CauldronScript component not found!");
                    // Return ingredient to its original position if cauldron script is missing
                    int index = System.Array.IndexOf(ingredients, draggedIngredient);
                    if (index != -1)
                    {
                        draggedIngredient.transform.position = ingredientPositions[index];
                    }
                }
            }


            else

            {
                // Return to starting position
                int index = System.Array.IndexOf(ingredients, draggedIngredient);
                if (index != -1)
                {
                    draggedIngredient.transform.position = ingredientPositions[index];
                }
            }

            draggedIngredient = null;
        }
    }

    private bool IsValidDropPosition(Vector3 position)
    {
        // Check if the ingredient is close enough to the cauldron
        float snapDistance = 2f; // Adjust this value to change how close ingredients need to be to snap
        return Vector2.Distance(position, cauldronPosition) < snapDistance;
    }


}
