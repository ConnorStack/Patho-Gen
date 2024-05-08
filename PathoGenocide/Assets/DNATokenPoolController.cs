using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DNATokenPoolController : MonoBehaviour
{
    public static DNATokenPoolController Instance;

    public GameObject dnaTokenPrefab;
    private ObjectPool<GameObject> dnaTokenPool;

    private void Awake()
    {
        Instance = this;
        InitializePool();
    }

    private void InitializePool()
    {
        dnaTokenPool = new ObjectPool<GameObject>(
            createFunc: CreateDNAToken,
            actionOnGet: (token) => token.SetActive(true),
            actionOnRelease: (token) => token.SetActive(false),
            actionOnDestroy: Destroy,
            defaultCapacity: 200,
            maxSize: 10000
        );

    }
    private GameObject CreateDNAToken()
    {
        return Instantiate(dnaTokenPrefab);
    }

    public GameObject GetDNAToken()
    {
        return dnaTokenPool.Get();
    }

    public void ReturnDNAToken(GameObject token)
    {
        dnaTokenPool.Release(token);
    }

}
