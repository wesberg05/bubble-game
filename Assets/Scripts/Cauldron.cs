using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public Ingredient currentIngredient;
    public float timer = 0f;
    private const float RESET_TIME = 3f;
    



    public Ingredient RemoveCurrentIngredient()
    {
        Ingredient previous = currentIngredient;
        currentIngredient = null;
        timer = 0f;
        return previous;
    }

    public void AddIngredient(Ingredient ingredient)
    {
        currentIngredient = ingredient;
        timer = RESET_TIME;
    }




    public bool HasIngredient()
    {
        return currentIngredient != null;
    }
}
