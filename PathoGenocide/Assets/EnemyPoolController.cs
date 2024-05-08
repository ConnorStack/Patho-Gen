using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPoolController : MonoBehaviour
{
    public static EnemyPoolController Instance;
    public GameObject enemyPrefab;
    private Queue<GameObject> enemies = new Queue<GameObject>();


    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetEnemy()
    {
        if (enemies.Count == 0)
        {
            AddEnemies(1);
        }

        return enemies.Dequeue();
    }

    private void AddEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemies.Enqueue(enemy);
        }
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemies.Enqueue(enemy);
    }
}
