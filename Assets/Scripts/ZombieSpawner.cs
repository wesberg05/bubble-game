using UnityEngine;

//hello

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject skeletonPrefab;
    private float spawnTimer = 0f;
    private const float SPAWN_INTERVAL = 2f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        
        if (spawnTimer >= SPAWN_INTERVAL)
        {
            SpawnSkeleton();
            spawnTimer = 0f;
        }
    }

    private void SpawnSkeleton()
    {
        // Convert screen bounds to world coordinates
        Vector2 screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        
        // Randomly choose which edge to spawn from (0: top, 1: right, 2: bottom, 3: left)
        int edge = Random.Range(0, 4);
        Vector3 spawnPosition = Vector3.zero;

        switch (edge)
        {
            case 0: // Top
                spawnPosition = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y, 0);
                break;
            case 1: // Right
                spawnPosition = new Vector3(screenBounds.x, Random.Range(-screenBounds.y, screenBounds.y), 0);
                break;
            case 2: // Bottom
                spawnPosition = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), -screenBounds.y, 0);
                break;
            case 3: // Left
                spawnPosition = new Vector3(-screenBounds.x, Random.Range(-screenBounds.y, screenBounds.y), 0);
                break;
        }

        Instantiate(skeletonPrefab, spawnPosition, Quaternion.identity);
    }
}
