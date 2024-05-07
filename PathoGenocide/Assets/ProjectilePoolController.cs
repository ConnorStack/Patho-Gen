using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoolController : MonoBehaviour
{
    public static ProjectilePoolController Instance { get; private set; }

    public GameObject projectilePrefab;
    private Queue<GameObject> projectilePool = new Queue<GameObject>();
    public int poolSize = 200;

    private void Awake()
    {
        Instance = this;
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, Vector3.zero, Quaternion.identity);
            projectile.SetActive(false);
            projectilePool.Enqueue(projectile);
            Debug.Log("Enqueue");
        }
    }

    public GameObject GetProjectile()
    {
        if (projectilePool.Count > 0)
        {
            // Debug.Log("Get Projectile, pool not empty");
            GameObject projectile = projectilePool.Dequeue();
            projectile.SetActive(true);
            return projectile;
        }
        else
        {
            // Debug.Log("Pool Empty");
            GameObject newProjectile = Instantiate(projectilePrefab, Vector3.zero, Quaternion.identity);
            return newProjectile;
        }
    }

    public void ReturnProjectile(GameObject projectile)
    {
        projectile.SetActive(false);
        projectilePool.Enqueue(projectile);
    }
}
