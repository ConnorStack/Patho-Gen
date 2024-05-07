using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 1f;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
    [SerializeField] private float startDelay = 0f;
    [SerializeField] private float spawnDuration = 20f;

    private float nextSpawnTime = 0f;
    private float spawnStartTime;
    private float spawnEndTime;
    private bool isSpawning = false;

    void Start()
    {
        spawnStartTime = Time.time + startDelay;
        spawnEndTime = spawnStartTime + spawnDuration;
    }

    void Update()
    {
        if (!isSpawning && Time.time >= spawnStartTime)
        {
            isSpawning = true;
            nextSpawnTime = Time.time;
        }

        if (isSpawning && Time.time <= spawnEndTime)
        {
            if (Time.time >= nextSpawnTime)
            {
                Debug.Log("Spawning enemy at random position");
                SpawnEnemyAtRandomLocation();
                nextSpawnTime = Time.time + spawnInterval;
            }
        }
        else if (isSpawning && Time.time > spawnEndTime)
        {
            Debug.Log("Spawner disabled");
            this.enabled = false;
        }
    }

    void SpawnEnemyAtRandomLocation()
    {
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}