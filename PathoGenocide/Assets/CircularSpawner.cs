using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 0.2f;
    public float rotationSpeed = 20f; // Degrees per second
    public float spawnRadius = 20f;
    public float spawnDuration = 20f; // Duration to spawn for
    private float nextSpawnTime;
    private float spawnStartTime; // Time when spawning should start
    private float spawnEndTime; // Time when spawning should end
    private bool isSpawningActive = false; // Is the spawner currently active?

    private void Start()
    {
        spawnStartTime = Time.time + 40f; // Set to start after 40 seconds
        spawnEndTime = spawnStartTime + spawnDuration; // Set to end after spawnDuration
    }

    private void Update()
    {
        // Check if within the spawn period
        if (Time.time >= spawnStartTime && Time.time <= spawnEndTime)
        {
            isSpawningActive = true;
        }
        else
        {
            isSpawningActive = false;
        }

        // If the spawner is active, move in a circle and spawn enemies
        if (isSpawningActive)
        {
            // Update the spawner's position to move in a circle
            float angle = (Time.time - spawnStartTime) * rotationSpeed; // Adjusted to start angle based on spawnStartTime
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * spawnRadius;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * spawnRadius;
            transform.position = new Vector3(x, y, 0f);

            if (Time.time >= nextSpawnTime)
            {
                SpawnEnemy();
                nextSpawnTime = Time.time + spawnRate;
            }
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