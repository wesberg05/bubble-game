using UnityEngine;

public class WitchDamageHandler : MonoBehaviour
{
    private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Take 1 damage when hit by an enemy
            healthSystem.TakeDamage(1);
        }
    }
}
