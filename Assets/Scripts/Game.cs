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
            // Check overlap with the collider in world space
            if (ingredients[i].GetComponent<BoxCollider2D>().OverlapPoint(mousePosition))
            {
                Debug.Log("Ingredient " + i + " clicked");
                // Update the ingredient's position
                ingredients[i].transform.position = cauldronPosition;
            }
        }
    }
}
}
