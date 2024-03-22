using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackHitbox;
    private bool isAttacking = false;
    private float cooldownTimer = 0;

    //other approach
    public Animator meleeAnimator;
    public Animator specialMeleeAnimator;
    public Transform meleeHitBox;
    [SerializeField] private float attackRange = 0.5f; // Horizontal reach
    [SerializeField] private float attackHeight = 0.5f; // Vertical reach, adjust as necessary

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

        // attackHitbox.SetActive(false); // Ensure the hitbox is disabled at the start
        // Debug.Log("attack hitbox set to false.");
    }


    void Update()
    {

        //I have to think of a way to give each attack its own cooldown. I dont want teh cooldowns
        //restricting each other. Each attack should have its own profile

        if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time >= nextBasicMeleeAttackTime)
        {
            Debug.Log("Basic Melee Attck");
            Attack(); //rename to basicAttack
            nextBasicMeleeAttackTime = Time.time + basicMeleeAttackCooldown;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextBasicRangedAttackTime)
        {
            Debug.Log("Basic Ranged Attack");
            nextBasicRangedAttackTime = Time.time + basicRangedAttackCooldown;
            //RangedAttack()
        }
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            Debug.Log("Special Melee");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Special Melee");
        }
        if (Input.GetKeyDown(KeyCode.Mouse3))
        {
            Debug.Log("Special Ranged");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Special Ranged");
        }

    }

    public void Attack()
    {
        meleeAnimator.SetTrigger("BasicAttack");

        Debug.Log("attack");
        attackVector = new Vector2(attackRange, attackHeight);
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(meleeHitBox.position, attackVector, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(basicAttack);
        }
        //https://www.youtube.com/watch?v=sPiVz1k-fEs&t=255s&ab_channel=Brackeys
    }

    public void SpecialMelee()
    {
        specialMeleeAnimator.SetTrigger("SpecialMelee");
    }

    private IEnumerator PerformAttack()
    {
        Debug.Log("Perform attack");
        isAttacking = true;
        attackHitbox.SetActive(true); // Activate the hitbox
        yield return new WaitForSeconds(0.1f); // Duration of the hitbox being active
        attackHitbox.SetActive(false); // Deactivate the hitbox
        Debug.Log("Deactivating hitbox");
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
