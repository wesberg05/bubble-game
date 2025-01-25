
using UnityEngine;
using System.Linq;




public class ZombieMovement : MonoBehaviour
{
    private Transform witchTransform;
    public float moveSpeed = 1f;
    private WitchHealthBar witchHealth;
    private bool canDealDamage = true;
    public GameObject leftIconDisplay;
    public GameObject rightIconDisplay;
    private float iconOffset = 1f; // Offset above zombie's head
    private float iconHorizontalSpacing = 0.25f; // Spacing between icons





    void Start()
    {
        // Find the witch GameObject and get required components
        GameObject witch = GameObject.Find("witch");
        if (witch != null)
        {
            witchTransform = witch.transform;
            witchHealth = witch.GetComponent<WitchHealthBar>();
        }

        // Get all ingredient prefabs
        GameObject[] ingredientPrefabs = GameObject.Find("Ingredients").GetComponentsInChildren<Ingredient>()
            .Select(i => i.gameObject).ToArray();
        
        if (ingredientPrefabs.Length > 0)
        {
            // Create first icon (left)
            GameObject selectedPrefab1 = ingredientPrefabs[Random.Range(0, ingredientPrefabs.Length)];
            leftIconDisplay = Instantiate(selectedPrefab1, 
                transform.position + Vector3.up * iconOffset + Vector3.left * iconHorizontalSpacing, 
                Quaternion.identity);
            leftIconDisplay.transform.SetParent(transform);
            leftIconDisplay.transform.localScale = new Vector3(2f, 2f, 2f);


            // Create second icon (right) - ensure it's different from the first
            GameObject selectedPrefab2;
            do
            {
                selectedPrefab2 = ingredientPrefabs[Random.Range(0, ingredientPrefabs.Length)];
            } while (selectedPrefab2 == selectedPrefab1);

            rightIconDisplay = Instantiate(selectedPrefab2, 
                transform.position + Vector3.up * iconOffset + Vector3.right * iconHorizontalSpacing, 
                Quaternion.identity);
            rightIconDisplay.transform.SetParent(transform);
            rightIconDisplay.transform.localScale = new Vector3(2f, 2f, 2f);

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

        // Update icons position
        if (leftIconDisplay != null)
        {
            leftIconDisplay.transform.position = transform.position + Vector3.up * iconOffset + Vector3.left * iconHorizontalSpacing;
        }
        if (rightIconDisplay != null)
        {
            rightIconDisplay.transform.position = transform.position + Vector3.up * iconOffset + Vector3.right * iconHorizontalSpacing;
        }

    }



    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        if (other.gameObject.name == "witch" && canDealDamage && witchHealth != null)
        {
            witchHealth.TakeDamage(1);
            if (leftIconDisplay != null)
            {
                Destroy(leftIconDisplay);
            }
            if (rightIconDisplay != null)
            {
                Destroy(rightIconDisplay);
            }

            Destroy(gameObject); // Destroy the skeleton upon contact
        }
    }



}
