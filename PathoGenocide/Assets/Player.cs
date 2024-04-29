using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    // private Animator bodyAnimator;
    // public GameOverMenuController gameOverController;
    // private int activeZones = 0;  // Count of active processing zones
    // // private float currentRate = 0f; 

    //
    // Player Basic Info
    [Header("Basic Info")]
    [SerializeField] private string playerName = "Leuk";

    // Movement Configuration
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxVerticalSpeed = 10;
    public enum PlayerMovementType { tf, physics };
    [SerializeField] private PlayerMovementType movementType = PlayerMovementType.tf;
    private Rigidbody2D rigidBody;

    // Health and Experience
    [Header("Stats")]
    public int currentHealth = 1000;
    public int currentLevel = 1;
    public int maxExpForLevel = 5;
    public int currentExp = 0;

    // DNA Tokens
    [Header("DNA Tokens")]
    public int dnaTokenCount = 0;
    private bool isProcessing = false;
    private int activeZones = 0;
    private int experienceIncreasePerToken = 1;

    // Animation
    [Header("Animation")]
    [SerializeField] private GameObject body;
    [SerializeField] private List<AnimationStateChanger> animationStateChangers;
    private Animator bodyAnimator;

    // UI and Game Over
    [Header("UI & Game Over")]
    public GameOverMenuController gameOverController;



    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        bodyAnimator = GetComponentInChildren<Animator>();
        UpdateLevelUI();
    }
    private void UpdateLevelUI()
    {
        UIManager.Instance.UpdateExperienceBar(currentHealth, maxExpForLevel);
        UIManager.Instance.UpdateLevel(currentLevel);
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
            DNATokenPoolController.Instance.ReturnDNAToken(other.gameObject);
            ReceiveToken();
        }
    }

    void ReceiveToken()
    {
        UIManager.Instance.UpdateTokenCount(dnaTokenCount);
    }

    public void EnterProcessingZone(float processingRate)
    {
        activeZones++;
        if (activeZones == 1)
        {
            StartProcessingTokens(processingRate);
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

    public void ExitProcessingZone()
    {
        activeZones--;
        if (activeZones == 0)
        {
            StopProcessingTokens();
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
            GainExperience(experienceIncreasePerToken);
            // currentExp++;  // Adjust based on desired experience increment
            UIManager.Instance.UpdateTokenCount(dnaTokenCount);
            UIManager.Instance.UpdateExperience(currentExp);

            // Debug.Log("Processed 1 token, Experience: " + currentExp);
            // Debug.Log("Token Count " + dnaTokenCount);
        }
    }

    private void GainExperience(int amount)
    {
        currentExp += amount;
        if (currentExp >= maxExpForLevel)
        {
            LevelUp();
        }
        UpdateLevelUI();
    }

    private void LevelUp()
    {
        currentExp -= maxExpForLevel;  // Carry over excess experience to the next level
        currentLevel++;
        maxExpForLevel = CalculateNextLevelExp(currentLevel);  // Calculate new threshold for next level
        // Debug.Log("Leveled up! New level: " + currentLevel);
    }

    private int CalculateNextLevelExp(int level)
    {
        // Example: Increase needed experience by 20% each level
        return (int)(level * 1.2f);
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
