
using UnityEngine;
using System.Linq;




public class SkeletonMovement : MonoBehaviour
{
    private Transform witchTransform;
    public float moveSpeed = 2f;
    private WitchHealthBar witchHealth;
    private bool canDealDamage = true;
    public GameObject iconDisplay;
    private float iconOffset = 1f; // Offset above skeleton's head



    void Start()
    {
        // Find the witch GameObject and get required components
        GameObject witch = GameObject.Find("witch");
        if (witch != null)
        {
            witchTransform = witch.transform;
            witchHealth = witch.GetComponent<WitchHealthBar>();
        }

        // Randomly select and create an ingredient icon
        GameObject[] ingredientPrefabs = GameObject.Find("Ingredients").GetComponentsInChildren<Ingredient>()
            .Select(i => i.gameObject).ToArray();
        
        if (ingredientPrefabs.Length > 0)
        {
            GameObject selectedPrefab = ingredientPrefabs[Random.Range(0, ingredientPrefabs.Length)];
            iconDisplay = Instantiate(selectedPrefab, transform.position + Vector3.up * iconOffset, Quaternion.identity);
            iconDisplay.transform.SetParent(transform);
            iconDisplay.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        }
    }


    void Update()
    {
        if (witchTransform != null)
        {
            // Move towards the witch
            Vector3 direction = (witchTransform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        // Update icon position
        if (iconDisplay != null)
        {
            iconDisplay.transform.position = transform.position + Vector3.up * iconOffset;
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        if (other.gameObject.name == "witch" && canDealDamage && witchHealth != null)
        {
            witchHealth.TakeDamage(1);
            if (iconDisplay != null)
            {
                Destroy(iconDisplay);
            }
            Destroy(gameObject); // Destroy the skeleton upon contact
        }
    }



}
