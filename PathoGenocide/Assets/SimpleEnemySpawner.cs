using UnityEngine;

public class SimpleEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    private float nextSpawnTime;
    private float startTime;
    private float activeDuration = 20f;
    private bool isActive = false;

    private void Start()
    {
        startTime = Time.time + 30f;
    }

    private void Update()
    {
        if (!isActive && Time.time >= startTime)
        {
            isActive = true;
            nextSpawnTime = Time.time + spawnInterval;
        }

        if (isActive && Time.time >= startTime + activeDuration)
        {
            isActive = false;
        }

        if (isActive && Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}