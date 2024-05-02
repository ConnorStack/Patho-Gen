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
            nextSpawnTime = Time.time + spawnInterval;
        }

        if (isActive && Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }

        if (isActive && Time.time >= startTime + activeDuration)
        {
            isActive = false;
            startTime = Time.time + spawnDelay;  // Reset startTime to reactivate after delay
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    public void ActivateSpawning()
    {
        isActive = true;
        startTime = Time.time + spawnDelay;
        nextSpawnTime = Time.time + spawnInterval;
    }
}