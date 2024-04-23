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
    public int dnaTokenCount = 0;
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
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DNAToken"))
        {
            dnaTokenCount++;
            Debug.Log($"Token received! Total tokens: {dnaTokenCount}");
            DNATokenPoolController.Instance.ReturnDNAToken(other.gameObject);
            ReceiveToken();
        }
    }

    void ReceiveToken()
    {
        Debug.Log("Token received!");
    }

    private void Die()
    {
        bodyAnimator.SetTrigger("Die");
        StartCoroutine(ShowDeathAnimationAndGameOver());
    }

    private IEnumerator ShowDeathAnimationAndGameOver()
    {
        yield return new WaitForSeconds(1);
        gameOverController.ShowGameOverMenu();
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuSceneName");
    }
}
