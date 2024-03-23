using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoolController : MonoBehaviour
{
    public static ProjectilePoolController Instance { get; private set; }

    public GameObject projectilePrefab;
    private Queue<GameObject> projectilePool = new Queue<GameObject>();
    public nint poolSize = 10;

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
        }
    }

    public GameObject GetProjectile()
    {
        if (poolSize > 0)
        {
            GameObject projectile = projectilePool.Dequeue();
            projectile.SetActive(true);
            return projectile;
        }
        else
        {
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
