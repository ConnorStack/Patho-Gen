using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 0.2f;
    [SerializeField] private Vector2 moveDirection = Vector2.right;
    public float moveSpeed = 7.5f;
    public float spawnDuration = 10f;
    [SerializeField] private float startDelay = 50f; // Delay before the spawner starts, settable in the editor
    private float nextSpawnTime;
    private float spawnEndTime;
    private float minX = -29f;
    private float maxX = 29f;

    private bool isSpawning = false; // Add a flag to control spawning

    private void Start()
    {
        StartCoroutine(DelayedStart(startDelay)); // Start the delayed activation
    }

    IEnumerator DelayedStart(float delay)
    {
        yield return new WaitForSeconds(delay);
        isSpawning = true; // Enable spawning after the delay
        spawnEndTime = Time.time + spawnDuration; // Calculate the end time based on the current time
        nextSpawnTime = Time.time + spawnRate; // Initialize the next spawn time
    }

    private void Update()
    {
        if (!isSpawning) return; // Exit Update if not yet time to spawn

        // Existing movement logic...
        transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);

        if (transform.position.x > maxX || transform.position.x < minX)
        {
            moveDirection.x = -moveDirection.x;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);
        }

        // Existing spawning logic...
        if (Time.time >= nextSpawnTime && Time.time <= spawnEndTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        // Existing enemy spawning logic...
        GameObject enemy = EnemyPoolController.Instance.GetEnemy();
        if (enemy != null)
        {
            enemy.transform.position = transform.position;
            enemy.SetActive(true);
        }
    }
}
