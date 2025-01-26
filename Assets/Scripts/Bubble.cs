using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Ingredient.TYPE type;
    public float floatSpeed = 2f;
    public float wobbleAmount = 0.1f;
    public float wobbleSpeed = 2f;
    
    private float startX;
    private float time;

    void Start()
    {
        startX = transform.position.x;
        time = Random.Range(0f, 10f); // Random start time for wobble
    }

    void Update()
    {
        // Move upward
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
        
        // Add wobble effect
        time += Time.deltaTime;
        float wobbleX = startX + Mathf.Sin(time * wobbleSpeed) * wobbleAmount;
        Vector3 pos = transform.position;
        pos.x = wobbleX;
        transform.position = pos;
        
        // Destroy if too high
        if (transform.position.y > Camera.main.orthographicSize + 1f)
        {
            Destroy(gameObject);
        }
    }
}
