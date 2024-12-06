using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;
    public float spawnRate = 1f; // Base spawn rate, adjusted by Quantity
    public float spawnRangeX = 5f; // Range along the x-axis for spawning

    private float nextSpawnTime = 0; // Tracks when the next spawn should occur

    private void Update()
    {
        if (GameManager.Instance.store.Quantity == 0)
        {
            return;
        }
        // Check if it's time to spawn a new prefab
        if (Time.time >= nextSpawnTime)
        {
            SpawnPrefab(); // Spawn a prefab based on efficiency
            nextSpawnTime = Time.time + 1f / GameManager.Instance.store.Quantity; // Adjust spawn rate by Quantity
        }
    }

    private void SpawnPrefab()
    {
        int efficiency = GameManager.Instance.store.Efficiency; // Get current efficiency

        // Select prefabs to spawn based on efficiency level
        GameObject prefabToSpawn = null;
        switch (efficiency)
        {
            case 0: // No spawning if efficiency is 0
                return;

            case 1: // Efficiency 1: Only prefab1
                prefabToSpawn = prefab1;
                break;

            case 2: // Efficiency 2: Randomly choose between prefab1 and prefab2
                prefabToSpawn = Random.value < 0.5f ? prefab1 : prefab2;
                break;

            default: // Efficiency 3: Randomly choose among prefab1, prefab2, and prefab3
                float rand = Random.value;
                if (rand < 0.33f)
                    prefabToSpawn = prefab1;
                else if (rand < 0.66f)
                    prefabToSpawn = prefab2;
                else
                    prefabToSpawn = prefab3;
                break;
        }

        // Spawn the selected prefab at a random position along the x-axis
        if (prefabToSpawn != null)
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.x += Random.Range(-spawnRangeX, spawnRangeX); // Randomize x position within the range
            spawnPosition.z = -2;
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    // Draw Gizmos to show the spawn line in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // Draw the line representing the spawn range
        Vector3 startLine = transform.position + Vector3.left * spawnRangeX;
        Vector3 endLine = transform.position + Vector3.right * spawnRangeX;
        Gizmos.DrawLine(startLine, endLine);

        // Optionally, draw a small sphere at the ends of the line for clarity
        Gizmos.DrawSphere(startLine, 0.2f);
        Gizmos.DrawSphere(endLine, 0.2f);
    }
}