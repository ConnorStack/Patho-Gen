using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 0.2f;
    [SerializeField] public Vector2 moveDirection = Vector2.right;
    public float moveSpeed = 7.5f;
    public float spawnDuration = 10f;
    private float nextSpawnTime;
    private float spawnEndTime;
    private float minX = -29f;
    private float maxX = 29f;

    private void Start()
    {
        spawnEndTime = Time.time + spawnDuration;
    }
    private void Update()
    {
        transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);


        if (transform.position.x > maxX || transform.position.x < minX)
        {
            moveDirection.x = -moveDirection.x;
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
        GameObject enemy = EnemyPoolController.Instance.GetEnemy(); // Fetch an enemy from the pool
        if (enemy != null)
        {
            enemy.transform.position = transform.position; // Position enemy at spawner's location
            enemy.SetActive(true); // Make the enemy active
        }
    }
}
