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
    public int currentHealth = 1000;
    public int currentExp = 0;
    public int maxExpForLevel = 10;
    public int dnaTokenCount = 0;
    private bool isProcessing = false;
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
    private int activeZones = 0;  // Count of active processing zones
    // private float currentRate = 0f; 


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
        UIManager.Instance.UpdateHealth(currentHealth);

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
            // Debug.Log($"Token received! Total tokens: {dnaTokenCount}");
            DNATokenPoolController.Instance.ReturnDNAToken(other.gameObject);
            ReceiveToken();
        }
    }

    void ReceiveToken()
    {
        //Some way 
        UIManager.Instance.UpdateTokenCount(dnaTokenCount);
        // UIManager.Instance.UpdateExperience(dnaTokenCount);
        // UIManager.Instance.UpdateExperienceBar(dnaTokenCount, maxExpForLevel);
    }

    public void EnterProcessingZone(float processingRate)
    {
        activeZones++;
        if (activeZones == 1)  // Start processing only if this is the first zone entered
        {
            StartProcessingTokens(processingRate);
        }
    }

    public void ExitProcessingZone()
    {
        activeZones--;
        if (activeZones == 0)  // Stop processing only if all zones are exited
        {
            StopProcessingTokens();
        }
    }

    private void StartProcessingTokens(float processingRate)
    {
        // currentRate = processingRate;  // Update the rate if needed
        if (!isProcessing)
        {
            isProcessing = true;
            StartCoroutine(ConvertTokensToExperience(processingRate));
        }
    }

    public void StopProcessingTokens()
    {
        isProcessing = false;
    }

    private IEnumerator ConvertTokensToExperience(float processingRate)
    {
        while (isProcessing && dnaTokenCount > 0)
        {
            yield return new WaitForSeconds(1f / processingRate);
            dnaTokenCount--;
            currentExp++;  // Adjust based on desired experience increment
            UIManager.Instance.UpdateTokenCount(dnaTokenCount);
            UIManager.Instance.UpdateExperience(currentExp);

            Debug.Log("Processed 1 token, Experience: " + currentExp);
            Debug.Log("Token Count " + dnaTokenCount);
        }
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
