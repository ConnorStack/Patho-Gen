using UnityEngine;

public class SimpleEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public float spawnInterval = 2f; // How often to spawn enemies
    private float nextSpawnTime;
    private float startTime; // When the spawner becomes active
    private float activeDuration = 20f; // Duration the spawner is active for
    private bool isActive = false; // Is the spawner currently active?

    private void Start()
    {
        // Set the spawner to become active after 30 seconds
        startTime = Time.time + 30f;
    }

    private void Update()
    {
        // Check if the spawner should become active
        if (!isActive && Time.time >= startTime)
        {
            isActive = true;
            nextSpawnTime = Time.time + spawnInterval; // Set the next spawn time
        }

        // Check if the spawner's active duration has elapsed
        if (isActive && Time.time >= startTime + activeDuration)
        {
            isActive = false; // Deactivate the spawner after 20 seconds of activity
        }

        // If the spawner is active and it's time to spawn a new enemy
        if (isActive && Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval; // Set the time for the next spawn
        }
    }

    void SpawnEnemy()
    {
        // Instantiate a new enemy at the spawner's position
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}