using UnityEngine;

public class GameController : MonoBehaviour
{
    public RandomSpawner randomSpawner;
    public CircularSpawner circularSpawner;
    public SimpleEnemySpawner simpleSpawner;
    public float phaseDuration = 5f;  // Each phase lasts 1 minute
    private float gameStartTime;

    private void Start()
    {
        gameStartTime = Time.time;
        // Initially disable spawners
        randomSpawner.enabled = false;
        circularSpawner.enabled = false;
        simpleSpawner.enabled = false;
    }

    private void Update()
    {
        float elapsedTime = Time.time - gameStartTime;

        // Phase 1: Random spawning
        if (elapsedTime <= phaseDuration)
        {
            randomSpawner.enabled = true;
            Debug.Log("Random spawner activate");
            // randomSpawner.UpdateSpawnParameters(0.2f, 5f); // Example parameters: spawn rate and speed
        }
        // Phase 2: Geometric patterns
        else if (elapsedTime <= phaseDuration * 2)
        {
            randomSpawner.enabled = false;
            circularSpawner.enabled = true;
            // circularSpawner.UpdateSpawnParameters(0.15f, 10f); // Adjusted for more frequent spawning
            simpleSpawner.enabled = true;
            Debug.Log("circularSpawner, simpleSpawner activate");
        }
        // Phase 3: Mixed patterns
        else if (elapsedTime <= phaseDuration * 3)
        {
            // Both spawners are active
        }
        // Phase 4: Prep for boss
        else if (elapsedTime <= phaseDuration * 4)
        {
            // Maybe introduce a new spawner or special enemy types
        }
        // Boss spawn
        else if (elapsedTime >= phaseDuration * 4)
        {
            // Spawn boss and disable other spawners
            SpawnBoss();
            randomSpawner.enabled = false;
            circularSpawner.enabled = false;
            simpleSpawner.enabled = false;
        }
    }

    void SpawnBoss()
    {
        // Code to spawn the boss
    }
}