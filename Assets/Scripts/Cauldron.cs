using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public Ingredient currentIngredient;
    public GameObject bubble;
    
    void Start()
    {

    }


    public Ingredient RemoveCurrentIngredient()
    {
        Ingredient previous = currentIngredient;
        currentIngredient = null;
        SpawnBubble();
        return previous;
    }

    public void AddIngredient(Ingredient ingredient)
    {
        currentIngredient = ingredient;
    }

    public bool HasIngredient()
    {
        return currentIngredient != null;
    }

    public void SpawnBubble()
    {
        Instantiate(bubble, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
    }
}
