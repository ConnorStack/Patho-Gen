using UnityEngine;

public class VirusBoss : MonoBehaviour
{
    public VirusBossLeg[] legs;
    public int totalHealth = 200;
    public float attackRadius = 5f; // The radius within which the attack affects the player
    public int attackDamage = 20; // The amount of damage dealt by each attack
    public float attackInterval = 3f; // Attack every 3 seconds
    private float nextAttackTime = 0f;
    public GameObject attackObject;
    private Animator attackAnimator;
    private DamageEffect damageEffect;

    void Start()
    {
        nextAttackTime = Time.time + attackInterval;
        if (attackObject != null)
        {
            attackAnimator = attackObject.GetComponent<Animator>();
            if (attackAnimator == null)
            {
                Debug.LogError("Animator component missing on attack object!");
            }
        }
        else
        {
            Debug.LogError("Attack object not assigned!");
        }
    }

    void Update()
    {
        // This will continuously check if the boss is vulnerable
        if (isVulnerable)
        {
            // Here, you could indicate the boss is vulnerable, e.g., change appearance
            Debug.Log("Boss is now vulnerable!");
            // Optionally, you can implement further behavior changes or effects here
        }
        if (Time.time >= nextAttackTime)
        {
            PerformAttack();
            nextAttackTime = Time.time + attackInterval;
        }
    }
    void PerformAttack()
    {
        // Debug log to show when the attack happens
        Debug.Log("Performing attack!");

        if (attackAnimator != null)
        {
            attackAnimator.SetTrigger("Attacking");
        }
        // Check for player within the attack radius
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player")) // Ensure the player has the tag "Player"
            {
                // Assuming the player has a script with a method to take damage
                hitCollider.GetComponent<Player>().TakeDamage(attackDamage);
            }
        }
    }

    public bool isVulnerable => CheckIfAllLegsDestroyed();

    void OnDrawGizmos()
    {
        // Draw the attack radius in the scene view to visualize it
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    bool CheckIfAllLegsDestroyed()
    {
        foreach (var leg in legs)
        {
            if (leg.gameObject.activeSelf)
                return false;
        }
        return true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the collider has the correct tag
        if (collider.CompareTag("PlayerAttack"))
        {
            int damage = 10;  // Example damage value, could be passed by the projectile

            TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isVulnerable)
        {
            totalHealth -= damage;
            if (totalHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Debug.Log("Boss defeated!");
        // You can add effects, animations or cleanup operations here
        Destroy(gameObject); // Destroys the boss object
    }


}