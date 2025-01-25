using UnityEngine;

public class GreenBubble : MonoBehaviour
{
    public float speed = 5f;
    private Transform target;
    private GameObject ingredientIcon;
    private float iconOffset = 1f;
    
    public void SetIngredient(Ingredient ingredient)
    {
        // Create a copy of the ingredient as an icon
        ingredientIcon = Instantiate(ingredient.gameObject, transform.position + Vector3.up * iconOffset, Quaternion.identity);
        ingredientIcon.transform.SetParent(transform);
        
        // Disable any colliders on the icon
        Collider2D collider = ingredientIcon.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
        
        // Scale the icon appropriately
        ingredientIcon.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void SetTarget(Transform enemyTransform)

    {
        target = enemyTransform;
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            
            // Check if we've reached the target
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget < 0.1f)
            {
                // Destroy both the bubble and the enemy
                Destroy(target.gameObject);
                if (ingredientIcon != null)
                {
                    Destroy(ingredientIcon);
                }
                Destroy(gameObject);
            }
        }
        else
        {
            // If target is destroyed before bubble reaches it
            if (ingredientIcon != null)
            {
                Destroy(ingredientIcon);
            }
            Destroy(gameObject);
        }

        // Update ingredient icon position
        if (ingredientIcon != null)
        {
            ingredientIcon.transform.position = transform.position + Vector3.up * iconOffset;
        }

    }
}
