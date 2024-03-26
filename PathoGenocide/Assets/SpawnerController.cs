using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject[] spawners;
    private int currentPhase = 0;
    private float phaseDuration = 60f;

    private void Start()
    {
        ActivateSpawner(0);
    }

    private void Update()
    {
        int phase = Mathf.FloorToInt(Time.time / phaseDuration);

        if (phase != currentPhase)
        {
            DeactivateSpawner(currentPhase);
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
