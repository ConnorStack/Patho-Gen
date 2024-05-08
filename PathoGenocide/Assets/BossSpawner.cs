using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public float spawnDelay = 120f; // Delay before the boss spawns, adjust as needed
    private bool bossSpawned = false;

    private void Start()
    {
        Invoke(nameof(SpawnBoss), spawnDelay);
    }

    void SpawnBoss()
    {
        if (!bossSpawned) // Ensure the boss spawns only once
        {
            GameObject boss = Instantiate(bossPrefab, transform.position, Quaternion.identity);
            bossSpawned = true;
            // Additional logic for boss introduction (animations, sound effects, etc.)
        }
    }
}