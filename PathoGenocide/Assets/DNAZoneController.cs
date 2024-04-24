using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNAZoneController : MonoBehaviour
{
    public GameObject zonePrefab;
    public float zoneLifetime = 10f;
    public float spawnInterval = 8f;
    public Vector2 mapBoundsMin;
    public Vector2 mapBoundsMax;
    private void Start()
    {
        // spawnInterval = 1f;
        InvokeRepeating("SpawnZone", 0f, spawnInterval);  // Adjust the spawn interval as needed
    }
    void SpawnZone()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(mapBoundsMin.x, mapBoundsMax.x),
            Random.Range(mapBoundsMin.y, mapBoundsMax.y)
        );

        GameObject zone = Instantiate(zonePrefab, spawnPosition, Quaternion.identity);
        Destroy(zone, zoneLifetime);
    }

}
