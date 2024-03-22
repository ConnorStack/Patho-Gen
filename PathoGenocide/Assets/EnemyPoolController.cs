using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolController : MonoBehaviour
{
    public static EnemyPoolController Instance;

    public GameObject enemyPrefab; // The enemy prefab
    private Queue<GameObject> enemies = new Queue<GameObject>(); // Pool of enemies

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetEnemy()
    {
        if (enemies.Count == 0) // If no enemies are available in the pool, create a new one
        {
            AddEnemies(1);
        }
        
        return enemies.Dequeue(); // Take an enemy from the pool
    }

    private void AddEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab); // Create a new enemy
            enemy.SetActive(false); // Initially deactivate it
            enemies.Enqueue(enemy); // Add it to the pool
        }
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false); // Deactivate the enemy
        enemies.Enqueue(enemy); // Return it to the pool
    }
}
