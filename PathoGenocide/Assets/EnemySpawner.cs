using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Array to hold different enemy prefabs
    public float spawnRate = 0.2f; // Time between spawns
    [SerializeField] public Vector2 moveDirection = Vector2.right; // Direction for spawner movement
    public float moveSpeed = 7.5f; // Speed at which the spawner moves
    public float spawnDuration = 10f; // How long the spawner is active
    private float nextSpawnTime;
    private float spawnEndTime;
    private float minX = -29f;
    private float maxX = 29f;

    private void Start()
    {
        spawnEndTime = Time.time + spawnDuration; // Calculate end time for spawning
    }
    private void Update()
    {
        transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);

        // Check if the spawner has reached the movement boundaries
        if (transform.position.x > maxX || transform.position.x < minX)
        {
            moveDirection.x = -moveDirection.x; // Reverse the movement direction
            // Adjust position to be within bounds to prevent overshooting
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);
        }

        if (Time.time >= nextSpawnTime && Time.time <= spawnEndTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        // int enemyIndex = Random.Range(0, enemyPrefabs.Length); // Select random enemy
        // Instantiate(enemyPrefabs[enemyIndex], transform.position, Quaternion.identity);
        // GameObject enemy = EnemyPoolController.Instance.GetEnemy();
        
        // // Set the enemy's position to the spawner's position.
        // enemy.transform.position = transform.position;

        // // Activate the enemy.
        // enemy.SetActive(true);
        GameObject enemy = EnemyPoolController.Instance.GetEnemy(); // Fetch an enemy from the pool
        if (enemy != null)
        {
            enemy.transform.position = transform.position; // Position enemy at spawner's location
            enemy.SetActive(true); // Make the enemy active
        }
    }
}
