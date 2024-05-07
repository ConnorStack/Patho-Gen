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
        // This will continuously check if the boss is vulnerable
        if (isVulnerable)
        {
            // Here, you could indicate the boss is vulnerable, e.g., change appearance
            Debug.Log("Boss is now vulnerable!");
            // Optionally, you can implement further behavior changes or effects here
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

    public void TakeDamage(int damage)
    {
        if (isVulnerable)
        {
            totalHealth -= damage;
            if (totalHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Debug.Log("Boss defeated!");
        // You can add effects, animations or cleanup operations here
        Destroy(gameObject); // Destroys the boss object
    }

    // void SpawnSpawner()
    // {
    //     if (enemySpawnerPrefab != null)
    //     {
    //         GameObject spawner = Instantiate(enemySpawnerPrefab, spawnPoint.position, Quaternion.identity);
    //         // spawner.SetActive(true); // Ensure the spawner is active when instantiated
    //         Debug.Log("Spawner Spawned");

    //         SimpleEnemySpawner spawnerScript = spawner.GetComponent<SimpleEnemySpawner>();
    //         if (spawnerScript != null)
    //         {
    //             // spawnerScript.ActivateSpawning(); // Activate spawning if needed
    //         }
    //     }
    // }
}