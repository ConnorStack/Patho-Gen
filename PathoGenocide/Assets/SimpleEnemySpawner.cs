using UnityEngine;

public class SimpleEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 10f;
    public float spawnDelay = 0f;
    private float nextSpawnTime;
    private float startTime;
    public float activeDuration = 20f;
    private bool isActive = false;

    private void Start()
    {
        startTime = Time.time + spawnDelay;
    }

    private void Update()
    {
        if (!isActive && Time.time >= startTime)
        {
            isActive = true;
            nextSpawnTime = Time.time;  // Start spawning immediately
        }

        if (isActive && Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;  // Set the time for the next spawn
        }

        if (isActive && Time.time >= startTime + activeDuration)
        {
            isActive = false;
            startTime = Time.time + spawnDelay + activeDuration;  // Reset startTime to reactivate after delay and duration
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}