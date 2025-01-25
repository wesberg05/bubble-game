using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public Ingredient currentIngredient;
    

    public Ingredient RemoveCurrentIngredient()
    {
        Ingredient previous = currentIngredient;
        currentIngredient = null;
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
}
