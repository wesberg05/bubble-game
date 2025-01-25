using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    private Transform witchTransform;
    public float moveSpeed = 2f;
    private WitchHealthBar witchHealth;
    private bool canDealDamage = true;


    void Start()
    {
        // Find the witch GameObject and get required components
        GameObject witch = GameObject.Find("witch");
        if (witch != null)
        {
            witchTransform = witch.transform;
            witchHealth = witch.GetComponent<WitchHealthBar>();
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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        if (other.gameObject.name == "witch" && canDealDamage && witchHealth != null)
        {
            witchHealth.TakeDamage(1);
            Destroy(gameObject); // Destroy the skeleton upon contact
        }
    }


}
