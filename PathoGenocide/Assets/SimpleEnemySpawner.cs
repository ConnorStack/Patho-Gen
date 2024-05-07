using UnityEngine;

public class SimpleEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 10f;
    [SerializeField] private float startDelay = 0f;  // Start delay before first spawn
    [SerializeField] private float spawnDuration = 20f;  // Duration the spawner is active

    private float nextSpawnTime = 0f;
    private float spawnStartTime;
    private float spawnEndTime;
    private bool isSpawning = false;

    void Start()
    {
        // Set the initial times based on the start delay and spawn duration
        spawnStartTime = Time.time + startDelay;
        spawnEndTime = spawnStartTime + spawnDuration;
    }

    void Update()
    {
        // Check if it's time to start spawning
        if (!isSpawning && Time.time >= spawnStartTime)
        {
            isSpawning = true;
            nextSpawnTime = Time.time;  // Initialize the next spawn time
        }

        // Handle active spawning within the duration
        if (isSpawning && Time.time <= spawnEndTime)
        {
            if (Time.time >= nextSpawnTime)
            {
                SpawnEnemy();
                nextSpawnTime = Time.time + spawnInterval;  // Set time for next spawn
            }
        }
        else if (isSpawning && Time.time > spawnEndTime)
        {
            // Optionally, disable spawner or reset for another cycle
            isSpawning = false;  // Stop spawning after duration ends
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}