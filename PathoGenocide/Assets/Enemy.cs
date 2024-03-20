using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
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
    }

}