using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 15f;
    public Vector2 direction;
    public bool isSpecial = false;
    public float specialDamage = 50f;

    // void Update()
    // {
    //     transform.Translate(direction.normalized * speed * Time.deltaTime);
    // }

    void FixedUpdate()
    {
        Vector2 movePosition = (Vector2)transform.position + (direction.normalized * speed * Time.fixedDeltaTime);
        GetComponent<Rigidbody2D>().MovePosition(movePosition);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag("Enemy"))
        {
            // Debug.Log("Projectile Collision with Enemy");
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(10);
            }
        }
        ProjectilePoolController.Instance.ReturnProjectile(gameObject);
    }

    private void OnBecameInvisible()
    {
        ProjectilePoolController.Instance.ReturnProjectile(gameObject);
    }
}
