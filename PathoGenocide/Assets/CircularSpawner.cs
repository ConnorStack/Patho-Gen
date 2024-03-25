using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 0.2f;
    public float rotationSpeed = 20f; // Degrees per second
    public float spawnRadius = 20f;
    public float spawnDuration = 10f;
    private float nextSpawnTime;
    private float spawnEndTime;

    private void Start()
    {
        spawnEndTime = Time.time + spawnDuration;
    }

    private void Update()
    {
        // Update the spawner's position to move in a circle
        float angle = Time.time * rotationSpeed;
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * spawnRadius;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * spawnRadius;
        transform.position = new Vector3(x, y, 0f);

        if (Time.time >= nextSpawnTime && Time.time <= spawnEndTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = EnemyPoolController.Instance.GetEnemy(); // Fetch an enemy from the pool
        if (enemy != null)
        {
            enemy.transform.position = transform.position; // Position enemy at spawner's location
            enemy.SetActive(true); // Make the enemy active
        }
    }
}
