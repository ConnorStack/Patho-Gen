using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;
    private Animator bodyAnimator;

    void Start()
    {
        currentHealth = maxHealth;
        bodyAnimator = GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("PlayerAttack"))
        {
            Debug.Log("taken damage");
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Health " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("death");
        bodyAnimator.SetTrigger("Die");
        EnemyPoolController.Instance.ReturnEnemy(gameObject);
    }
}
