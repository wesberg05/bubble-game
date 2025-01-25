using UnityEngine;

//[System.Serializable]
public class Game : MonoBehaviour
{
    public Ingredient[] ingredients;

    public GameObject cauldron;

    public GameObject player;
    public Vector3 cauldronPosition;
    
    public Vector3[] ingredientPositions;
    public Vector3 mousePosition;
    
    private bool isDragging = false;
    private Ingredient draggedIngredient = null;


/*************  ✨ Codeium Command ⭐  *************/
/// <summary>
/// Initializes the ingredientPositions array with the current positions of all ingredients.
/// </summary>

/******  be5eacb6-9f2e-469b-b95d-2d3e220a3ddf  *******/
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
        
        if (isDragging && draggedIngredient != null)
        {
            draggedIngredient.transform.position = mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && draggedIngredient != null)
        {
            isDragging = false;
            
            // Check if the ingredient should snap to cauldron
            if (IsValidDropPosition(draggedIngredient.transform.position))
            {
                draggedIngredient.transform.position = cauldronPosition;
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

    private Vector3 SnapToGrid(Vector3 position)
    {
        float snappedX = Mathf.Round(position.x);
        float snappedY = Mathf.Round(position.y);
        return new Vector3(snappedX, snappedY, position.z);
    }

    private bool IsValidDropPosition(Vector3 position)
    {
        // Check if the ingredient is close enough to the cauldron
        float snapDistance = 2f; // Adjust this value to change how close ingredients need to be to snap
        return Vector2.Distance(position, cauldronPosition) < snapDistance;
    }


}
