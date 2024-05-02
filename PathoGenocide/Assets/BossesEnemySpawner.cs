using UnityEngine;

public class BossesEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 10f;     // Time between each enemy spawn
    public float waveInterval = 30f;      // Time between waves of spawns
    public float activeDuration = 20f;    // Duration of active spawning per wave

    private float nextSpawnTime = 0f;
    private float nextWaveStartTime = 0f;
    private float waveEndTime = 0f;
    private bool isSpawningActive = false;

    private void Start()
    {
        nextWaveStartTime = Time.time + waveInterval; // Initial delay before starting the first wave
    }

    private void Update()
    {
        if (!isSpawningActive && Time.time >= nextWaveStartTime)
        {
            StartWave();
        }

        if (isSpawningActive)
        {
            if (Time.time >= nextSpawnTime)
            {
                SpawnEnemy();
                nextSpawnTime = Time.time + spawnInterval;
            }

            if (Time.time >= waveEndTime)
            {
                EndWave();
            }
        }
    }

    private void StartWave()
    {
        isSpawningActive = true;
        nextSpawnTime = Time.time;  // Start spawning immediately
        waveEndTime = Time.time + activeDuration; // Set end time for this wave
    }

    private void EndWave()
    {
        isSpawningActive = false;
        nextWaveStartTime = Time.time + waveInterval; // Set start time for the next wave
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    public void ActivateSpawning()
    {
        StartWave();
    }
}
