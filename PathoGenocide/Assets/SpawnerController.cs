using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject[] spawners; // Assign your spawners in the inspector
    private int currentPhase = 0;
    private float phaseDuration = 60f; // Duration of each phase in seconds

    private void Start()
    {
        ActivateSpawner(0); // Activate the first spawner at the start
    }

    private void Update()
    {
        // Determine the current phase based on the elapsed time
        int phase = Mathf.FloorToInt(Time.time / phaseDuration);

        // Check if we've moved to the next phase
        if (phase != currentPhase)
        {
            // Deactivate the previous spawner
            DeactivateSpawner(currentPhase);
            // Activate the next spawner
            ActivateSpawner(phase);
            currentPhase = phase;
        }
    }

    void ActivateSpawner(int index)
    {
        if (index < spawners.Length)
        {
            spawners[index].SetActive(true);
        }
    }

    void DeactivateSpawner(int index)
    {
        if (index < spawners.Length)
        {
            spawners[index].SetActive(false);
        }
    }
}
