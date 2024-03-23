using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 direction;

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        ProjectilePoolController.Instance.ReturnProjectile(gameObject);
    }

    private void OnBecameInvisible()
    {
        ProjectilePoolController.Instance.ReturnProjectile(gameObject);
    }
}
