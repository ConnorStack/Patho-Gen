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

    public Vector2 attackVector;
    public LayerMask enemyLayers;

    public int basicAttack = 10;

    public float nextAttackTime = 0f;
    [SerializeField] public float basicMeleeAttackCooldown = 1.0f;
    [SerializeField] public float basicRangedAttackCooldown = 0f;
    [SerializeField] public float specialMeleeCooldown = 0f;
    [SerializeField] public float specialRangedCooldown = 0f;
    private float nextBasicMeleeAttackTime = 0f;
    private float nextBasicRangedAttackTime = 0f;
    private float nextSpecialMeleeAttackTime = 0f;
    private float nextSpecialRangedAttackTime = 0f;

    public float attackCooldown = 2.5f;

    void Start()
    {
        bodyAnimator = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        HandleBasicMeleeAttack();
        HandleBasicRangedAttack();
        HandleSpecialMeleeAttack();
    }

    void HandleBasicMeleeAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time >= nextBasicMeleeAttackTime)
        {
            // Debug.Log("Basic Melee Attack");
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextBasicRangedAttackTime)
        {
            Debug.Log("Activate Ranged Attack");
            PerformBasicRangedAttack();
            nextBasicRangedAttackTime = Time.time + basicRangedAttackCooldown;
        }
    }

    void PerformBasicRangedAttack()
    {
        Debug.Log("Perform Ranged Attack");
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure the z position is not affecting the direction

        Vector2 shootingDirection = mousePosition - projectileOrigin.position;

        GameObject projectile = Instantiate(projectilePrefab, projectileOrigin.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().direction = shootingDirection;
    }

    void HandleSpecialMeleeAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextSpecialMeleeAttackTime)
        {
            Debug.Log("Handling spec melee");
            PerformSpecialMeleeAttack();
            nextSpecialMeleeAttackTime = Time.time + specialMeleeCooldown;
        }
    }

    void PerformSpecialMeleeAttack()
    {
        Debug.Log("Perform Special Melee Attack");
        specialMeleeAnimator.SetTrigger("SpecialMelee");

    }

    public void OnDrawGizmosSelected()
    {
        if (meleeHitBox == null)
        {
            return;
        }
        // Assuming attackVector is the size of the box
        Gizmos.DrawWireCube(meleeHitBox.position, new Vector3(attackRange, attackHeight, 0));
    }
}
