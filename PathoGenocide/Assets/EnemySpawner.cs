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
    [SerializeField] private float startDelay = 50f;
    private float nextSpawnTime;
    private float spawnEndTime;
    private float minX = -29f;
    private float maxX = 29f;

    private bool isSpawning = false;

    private void Start()
    {
        StartCoroutine(DelayedStart(startDelay));
    }

    IEnumerator DelayedStart(float delay)
    {
        yield return new WaitForSeconds(delay);
        isSpawning = true;
        spawnEndTime = Time.time + spawnDuration;
        nextSpawnTime = Time.time + spawnRate;
    }

    private void Update()
    {
        if (!isSpawning) return;
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
        GameObject enemy = EnemyPoolController.Instance.GetEnemy();
        if (enemy != null)
        {
            enemy.transform.position = transform.position;
            enemy.SetActive(true);
        }
    }
}
