using UnityEngine;

public class VirusBoss : MonoBehaviour
{
    public VirusBossLeg[] legs;
    public int totalHealth = 200;
    public bool isVulnerable => CheckIfAllLegsDestroyed();
    public GameObject enemySpawnerPrefab; // Assign this in the Inspector
    public Transform spawnPoint;          // Optional: A specific point for spawning spawners
    public float spawnSpawnerInterval = 10f; // Interval between spawning spawners
    private float nextSpawnTime = 0f;
    void Update()
    {
        if (isVulnerable)
        {
            // Change behavior, make boss vulnerable or change attack patterns
            // if (Time.time >= nextSpawnTime)
            // {
            //     SpawnSpawner();
            //     nextSpawnTime = Time.time + spawnSpawnerInterval;
            // }
        }
        if (Time.time >= nextSpawnTime)
        {
            SpawnSpawner();
            nextSpawnTime = Time.time + spawnSpawnerInterval;
        }
    }

    void Start()
    {
        nextSpawnTime = Time.time + 5.0f;
        // InvokeRepeating("SpawnSpawner", 3.0f, 8.0f);
    }

    bool CheckIfAllLegsDestroyed()
    {
        foreach (var leg in legs)
        {
            if (leg.gameObject.activeSelf)
                return false;
        }
        return true;
    }

    void SpawnSpawner()
    {
        if (enemySpawnerPrefab != null)
        {
            GameObject spawner = Instantiate(enemySpawnerPrefab, spawnPoint.position, Quaternion.identity);
            // spawner.SetActive(true); // Ensure the spawner is active when instantiated
            Debug.Log("Spawner Spawned");

            SimpleEnemySpawner spawnerScript = spawner.GetComponent<SimpleEnemySpawner>();
            if (spawnerScript != null)
            {
                spawnerScript.ActivateSpawning(); // Activate spawning if needed
            }
        }
    }
}