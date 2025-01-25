using Unity.VisualScripting;
using UnityEngine;

//[System.Serializable]
public class Game : MonoBehaviour
{

    //init scripts and game objects
    public Ingredient[] ingredients;
    public GameObject cauldron;
    public GameObject player;

    //init positions
    private Vector3 cauldronPosition;
    private Vector3 mousePosition;
    private Vector3[] ingredientPositions;
    
    //init drag/drop 
    private Ingredient draggedIngredient = null;
    private bool isDragging = false;

    [SerializeField]
    private float cooldown;

    private void Start()
    {
        cauldronPosition = cauldron.transform.position;

        ingredientPositions = new Vector3[ingredients.Length];
        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredientPositions[i] = ingredients[i].transform.position;
        }
    }

    private void Update()
    {
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
                draggedIngredient.transform.position = cauldronPosition;
            }
            else if (!IsValidDropPosition(draggedIngredient.transform.position) && cooldown <= 0)
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
