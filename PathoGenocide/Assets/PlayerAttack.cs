using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Animator meleeAnimator;
    public Animator specialMeleeAnimator;
    private Animator bodyAnimator;
    public Transform meleeHitBox;
    public Transform projectileOrigin;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackHeight = 0.5f;
    [SerializeField] float specialAttackRadius = 3.0f;

    public Vector2 attackVector;
    public LayerMask enemyLayers;

    public int basicAttack = 10;

    public float nextAttackTime = 0f;
    [SerializeField] public float basicMeleeAttackCooldown = 1.0f;
    [SerializeField] public float basicRangedAttackCooldown = 1.0f;
    [SerializeField] public float specialMeleeCooldown = 0f;
    [SerializeField] public float specialRangedCooldown = 0f;
    private float nextBasicMeleeAttackTime = 1.0f;
    private float nextBasicRangedAttackTime = 0f;
    private float nextSpecialMeleeAttackTime = 0f;
    private float nextSpecialRangedAttackTime = 0f;

    public float attackCooldown = 2.5f;

    private float currentMeleeCooldown;
    private float currentRangedCooldown;
    private Player player;

    void Start()
    {
        bodyAnimator = GetComponentInChildren<Animator>();
        player = GetComponent<Player>();
        UpdateCooldowns();
        if (player == null)
        {
            Debug.LogError("Player component not found on the GameObject!");
        }
    }


    void Update()
    {
        UpdateCooldowns();
        HandleBasicMeleeAttack();
        HandleBasicRangedAttack();
        HandleSpecialMeleeAttack();
        HandleSpecialRangedAttack();
    }

    void UpdateCooldowns()
    {
        currentMeleeCooldown = basicMeleeAttackCooldown / (1 + 0.1f * (player.currentLevel - 1));  // Reduces by 10% each level
        currentRangedCooldown = basicRangedAttackCooldown / (1 + 0.1f * (player.currentLevel - 1));  // Reduces by 10% each level
    }

    void HandleBasicMeleeAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time >= nextBasicMeleeAttackTime)
        {
            PerformBasicMeleeAttack();
            nextBasicMeleeAttackTime = Time.time + basicMeleeAttackCooldown;
        }
    }

    void PerformBasicMeleeAttack()
    {
        Debug.Log("Performing Melee Attack Animation");
        meleeAnimator.SetTrigger("BasicMelee");
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(meleeHitBox.position, new Vector2(attackRange, attackHeight), 0, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(basicAttack);
            }
            else
            {
                Debug.LogWarning("Hit object does not have an Enemy component.", enemy.gameObject);
            }
        }
    }

    void HandleBasicRangedAttack()
    {

        if (Time.time >= nextBasicRangedAttackTime)
        {
            PerformBasicRangedAttack();
            nextBasicRangedAttackTime = Time.time + currentRangedCooldown; // Use the current cooldown based on level
        }
    }

    void PerformBasicRangedAttack()
    {
        // Debug.Log("Perform Ranged Attack");
        GameObject projectile = ProjectilePoolController.Instance.GetProjectile();
        projectile.transform.position = transform.position;
        projectile.GetComponent<Projectile>().direction = CalculateShootingDirection();

    }
    Vector2 CalculateShootingDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector2 shootingDirection = mousePosition - projectileOrigin.position;
        return shootingDirection;
    }

    void HandleSpecialMeleeAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextSpecialMeleeAttackTime)
        {
            // Debug.Log("Handling spec melee");
            PerformSpecialMeleeAttack();
            nextSpecialMeleeAttackTime = Time.time + specialMeleeCooldown;
        }
    }

    void PerformSpecialMeleeAttack()
    {
        // Debug.Log("Perform Special Melee Attack");
        specialMeleeAnimator.SetTrigger("SpecialMelee");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, specialAttackRadius, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(basicAttack);
            }
        }
    }

    void HandleSpecialRangedAttack()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time >= nextSpecialRangedAttackTime)
        {
            // Debug.Log("Handling spec ranged");
            PerformSpecialRangedAttack();
        }
    }

    void PerformSpecialRangedAttack()
    {
        // Debug.Log("Performing spec ranged");
    }

    public void OnDrawGizmosSelected()
    {
        if (meleeHitBox == null)
        {
            return;
        }

        Gizmos.DrawWireCube(meleeHitBox.position, new Vector3(attackRange, attackHeight, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, specialAttackRadius);
    }
}
