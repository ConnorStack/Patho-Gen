using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign in Inspector
    public float spawnInterval = 1f; // Time between spawns
    public Vector2 spawnAreaMin; // Bottom-left corner of the spawn area
    public Vector2 spawnAreaMax; // Top-right corner of the spawn area
    [SerializeField] private float startDelay = 0f; // Delay before starting the spawn, settable in editor
    [SerializeField] private float spawnDuration = 20f; // Duration of the spawning phase, settable in editor

    private float nextSpawnTime = 0f;
    private float spawnStartTime; // Time when the spawning starts
    private float spawnEndTime; // Time when the spawning should end
    private bool isSpawning = false; // To check if we should start spawning

    void Start()
    {
        // Set the start and end times based on current time + delay
        spawnStartTime = Time.time + startDelay;
        spawnEndTime = spawnStartTime + spawnDuration;
    }

    void Update()
    {
        // Check if it's time to start spawning
        if (!isSpawning && Time.time >= spawnStartTime)
        {
            isSpawning = true;
            nextSpawnTime = Time.time; // Initialize next spawn time
        }

        // Spawn enemies if within the spawn duration
        if (isSpawning && Time.time <= spawnEndTime)
        {
            if (Time.time >= nextSpawnTime)
            {
                SpawnEnemyAtRandomLocation();
                nextSpawnTime = Time.time + spawnInterval;
            }
        }
        else if (isSpawning && Time.time > spawnEndTime)
        {
            // Optionally disable this script if the spawn duration has ended
            this.enabled = false;
        }
    }

    void SpawnEnemyAtRandomLocation()
    {
        // Generate a random position within the defined spawn area
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        // Instantiate the enemy at the generated random position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}