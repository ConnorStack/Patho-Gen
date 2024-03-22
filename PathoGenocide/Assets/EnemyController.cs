using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public float speed = 8f;
    private Transform playerTransform;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void FixedUpdate()
    {
        if (playerTransform != null)
        {
            // Calculate direction vector from enemy to player
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            // Move the enemy towards the player
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }
}
