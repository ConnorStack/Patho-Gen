using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 0.2f;
    public float rotationSpeed = 20f;
    public float spawnRadius = 20f;
    public float spawnDuration = 20f;
    private float nextSpawnTime;
    private float spawnStartTime;
    private float spawnEndTime;
    private bool isSpawningActive = false;
    [SerializeField] private float waitTime = 40f;

    private void Start()
    {
        spawnStartTime = Time.time + waitTime;
        spawnEndTime = spawnStartTime + spawnDuration;
    }

    private void Update()
    {
        if (Time.time >= spawnStartTime && Time.time <= spawnEndTime)
        {
            isSpawningActive = true;
        }
        else
        {
            isSpawningActive = false;
        }

        if (isSpawningActive)
        {
            float angle = (Time.time - spawnStartTime) * rotationSpeed;
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
        GameObject enemy = EnemyPoolController.Instance.GetEnemy();
        if (enemy != null)
        {
            enemy.transform.position = transform.position;
            enemy.SetActive(true);
        }
    }
}