using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class Enemy : MonoBehaviour
// {
//     public int maxHealth = 10;
//     private int currentHealth;
//     private Animator bodyAnimator;

//     void Start()
//     {
//         currentHealth = maxHealth;
//         bodyAnimator = GetComponentInChildren<Animator>();
//     }

//     void OnTriggerEnter2D(Collider2D collider)
//     {
//         if (collider.gameObject.CompareTag("PlayerAttack"))
//         {
//             Debug.Log("taken damage");
//             TakeDamage(10);
//         }

//         else if (collider.gameObject.CompareTag("Player"))
//         {
//             Player player = collider.GetComponent<Player>();
//             if (player != null)
//             {
//                 int damageAmount = 10; // The damage this enemy deals to the player
//                 player.TakeDamage(damageAmount);
//             }
//             Die(); // The enemy dies after dealing damage to the player
//         }
//     }


//     public void TakeDamage(int damage)
//     {

//         currentHealth -= damage;
//         Debug.Log("Enemy has taken damage. Health " + currentHealth);
//         if (currentHealth <= 0)
//         {
//             Die();
//         }
//     }

//     void Die()
//     {
//         Debug.Log("Enemy dies");
//         bodyAnimator.SetTrigger("Die");
//         StartCoroutine(WaitAndReturnToPool(0.30f)); // Wait for 0.30 seconds
//     }

//     IEnumerator WaitAndReturnToPool(float waitTime)
//     {
//         // Wait for the length of the animation
//         yield return new WaitForSeconds(waitTime);

//         // Return the enemy to the pool after the animation has played
//         EnemyPoolController.Instance.ReturnEnemy(gameObject);
//     }
// }

public class Enemy : MonoBehaviour
{
    public int maxHealth = 10;
    public bool followPlayer = true; // Toggle this in the Inspector
    public float moveSpeed = 5f; // Speed at which the enemy moves towards the player

    private int currentHealth;
    private Animator bodyAnimator;
    private Transform playerTransform; // To store the player's transform

    void Start()
    {
        currentHealth = maxHealth;
        bodyAnimator = GetComponentInChildren<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Assuming your player has the tag "Player"
    }

    void Update()
    {
        // Follow player if toggled on
        if (followPlayer && playerTransform != null)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        // Simple follow logic - moves towards the player each frame
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("PlayerAttack"))
        {
            // Debug.Log("taken damage");
            TakeDamage(10);
        }
        else if (collider.gameObject.CompareTag("Player"))
        {
            Player player = collider.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(10); // Example damage amount
            }
            Die(); // Optionally, remove this if enemies should not die upon colliding with the player
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Debug.Log("Enemy dies");
        bodyAnimator.SetTrigger("Die");
        StartCoroutine(WaitAndReturnToPool(0.30f));
    }

    IEnumerator WaitAndReturnToPool(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        EnemyPoolController.Instance.ReturnEnemy(gameObject);
    }
}