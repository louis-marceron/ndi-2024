using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public Transform startPoint;
    public CheckpointType checkpointType;
    public Checkpoint nextCheckpoint;
    public GameObject prefab;
    public int spawnRate;

    private Coroutine spawnCoroutine;

    private void Start()
    {
        // Start the spawning process
        StartSpawning();
    }

    private void StartSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine); // Stop the previous coroutine if it's running
        }
        spawnCoroutine = StartCoroutine(SpawnPrefabRoutine());
    }

    private IEnumerator SpawnPrefabRoutine()
    {
        while (true) // Continue spawning indefinitely
        {
            SpawnPrefab();
            yield return new WaitForSeconds(1f / spawnRate); // Wait for the time defined by spawnRate
        }
    }

    private void SpawnPrefab()
    {
        if (prefab != null)
        {
            // Instantiate the prefab at the start point with the defined rotation
            GameObject instance = Instantiate(prefab, startPoint.position, startPoint.rotation);
            Spawned spawned = instance.GetComponent<Spawned>();
            spawned.nextCheckpoint = nextCheckpoint;
        }
    }
}