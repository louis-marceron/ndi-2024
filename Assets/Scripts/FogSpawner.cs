using UnityEngine;

public class FogSpawner : MonoBehaviour
{
    public GameObject fogPrefab1;
    public GameObject fogPrefab2;
    public GameObject fogPrefab3;
    public float coneAngle = 45f;        // Angle of the cone (in degrees)
    public float coneRange = 10f;        // Maximum range (distance) for fog to move
    public float spawnRate = 0.1f;      // Time interval for spawning fog
    public float fogSpeed = 1f;         // Speed of fog movement
    public float minLifetime = 2f;      // Minimum lifetime of fog
    public float maxLifetime = 5f;      // Maximum lifetime of fog

    private float lastSpawnTime;         // Timer to track spawn rate

    private void Update()
    {
        // Time-based spawning control
        if (Time.time - lastSpawnTime >= spawnRate)
        {
            SpawnFog();
            lastSpawnTime = Time.time;
        }
    }

    private void SpawnFog()
    {
        // Spawn the fog at the spawner's position
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, -1);

        // Select a random fog prefab from the three
        GameObject selectedFogPrefab = GetRandomFogPrefab();

        // Instantiate the fog prefab at the starting position
        GameObject fog = Instantiate(selectedFogPrefab, spawnPosition, Quaternion.identity);

        // Calculate a random direction vector within the cone
        Vector2 randomDirection = GetRandomDirectionInCone();

        // Apply the random direction to the fog's velocity
        Rigidbody2D rb = fog.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = randomDirection * fogSpeed; // Apply random movement direction
        }

        // Set a random lifetime for the fog and destroy it after the duration
        float lifetime = Random.Range(minLifetime, maxLifetime);
        Destroy(fog, lifetime);
    }

    private Vector2 GetRandomDirectionInCone()
    {
        // Randomly pick an angle within the cone's angle range (relative to upward direction)
        float randomAngle = Random.Range(-coneAngle / 2f, coneAngle / 2f);

        // Convert the angle to radians (but keep the angle along the Y-axis)
        float angleInRadians = Mathf.Deg2Rad * randomAngle;

        // Create a random direction vector based on the angle, ensuring movement is along the Y-axis
        Vector2 direction = new Vector2(Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians));

        // Normalize the direction to make sure all fog particles move at the same speed
        return direction.normalized;
    }

    private GameObject GetRandomFogPrefab()
    {
        int randomIndex = Random.Range(0, 3); // Random index between 0 (inclusive) and 3 (exclusive)
        switch (randomIndex)
        {
            case 0: return fogPrefab1;
            case 1: return fogPrefab2;
            case 2: return fogPrefab3;
            default: return fogPrefab1; // Default case, though it won't be hit
        }
    }

    // Draw the Gizmo for the cone in the Scene view
    private void OnDrawGizmos()
    {
        // Color for the cone
        Gizmos.color = Color.green;

        // Draw the cone shape: start from the spawner's position and spread out to the defined range and angle
        Vector3 start = transform.position;

        // Calculate the left and right edge points (spread from the top)
        Vector3 leftEdge = start + new Vector3(Mathf.Sin(Mathf.Deg2Rad * (coneAngle / 2f)) * coneRange, Mathf.Cos(Mathf.Deg2Rad * (coneAngle / 2f)) * coneRange, 0);
        Vector3 rightEdge = start + new Vector3(Mathf.Sin(Mathf.Deg2Rad * (-coneAngle / 2f)) * coneRange, Mathf.Cos(Mathf.Deg2Rad * (-coneAngle / 2f)) * coneRange, 0);

        // Draw lines to show the cone's boundary
        Gizmos.DrawLine(start, leftEdge);
        Gizmos.DrawLine(start, rightEdge);

        // Draw the cone's "base" line (the bottom part of the cone's range)
        Gizmos.DrawLine(leftEdge, rightEdge);
    }
}