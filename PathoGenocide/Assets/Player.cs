using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float speed = 5f;
    [SerializeField] float maxVerticalSpeed = 10;
    public int currentHealth = 100;
    public enum PlayerMovementType { tf, physics };
    [SerializeField] PlayerMovementType movementType = PlayerMovementType.tf;
    [Header("Physics")]
    [Header("Flavor")]
    [SerializeField] string playerName = "Leuk";
    [SerializeField] private GameObject body;
    [SerializeField] List<AnimationStateChanger> animationStateChangers;
    Rigidbody2D rigidBody;
    private Animator bodyAnimator;
    public GameOverMenuController gameOverController;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        bodyAnimator = GetComponentInChildren<Animator>();
    }

    public void MovePlayer(Vector3 direction)
    {
        MovePlayerRigidBody(direction);
    }

    public void MovePlayerRigidBody(Vector3 direction)
    {
        Vector3 desiredVelocity = new Vector3(direction.x * speed, direction.y * speed, 0);
        desiredVelocity.y = Mathf.Clamp(desiredVelocity.y, -maxVerticalSpeed, maxVerticalSpeed);
        rigidBody.velocity = desiredVelocity;

        if (direction.x < 0)
        {
            body.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction.x > 0)
        {
            body.transform.localScale = new Vector3(1, 1, 1);
        }

        if (direction != Vector3.zero)
        {
            bodyAnimator.SetTrigger("Walk");
        }
        else
        {
            bodyAnimator.SetTrigger("Idle");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        // Debug.Log("Player has taken damage. Health " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        bodyAnimator.SetTrigger("Die");
        // Debug.Log("Player died.");
        // gameOverController.ShowGameOverMenu();
        StartCoroutine(ShowDeathAnimationAndGameOver());
    }

    private IEnumerator ShowDeathAnimationAndGameOver()
    {
        // Assuming the death animation lasts for 2 seconds
        yield return new WaitForSeconds(1);
        gameOverController.ShowGameOverMenu();
        // Freeze the game
        // Time.timeScale = 0;

        // Activate the Game Over menu
        // gameOverMenu.SetActive(true); // Make sure you have a reference to your Game Over menu canvas
    }

    public void ExitToMainMenu()
    {
        // Unfreeze the game
        Time.timeScale = 1;

        // Load your main menu scene by its name
        SceneManager.LoadScene("MainMenuSceneName");
    }
}
