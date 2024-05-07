using UnityEngine;

public class VirusBossLeg : MonoBehaviour
{
    public int legHealth = 50;
    private DamageEffect damageEffect;
    public VirusBoss boss; // Reference to the boss

    void Start()
    {
        damageEffect = GetComponent<DamageEffect>();
        if (damageEffect == null)
            Debug.LogError("DamageEffect script not found on the leg!");

        boss = GetComponentInParent<VirusBoss>(); // Assuming the boss is the parent of the legs
        if (boss == null)
            Debug.LogError("VirusBoss script not found in parent!");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerAttack")) // Ensure your projectiles are tagged correctly
        {
            TakeDamage(10); // Damage value should be passed by the projectile ideally
        }
    }

    void TakeDamage(int damage)
    {
        legHealth -= damage;
        if (legHealth <= 0)
        {
            DestroyLeg();
        }
        else
        {
            damageEffect.TakeDamage();  // Trigger the visual effect
        }
    }

    void DestroyLeg()
    {
        // Disable leg, play destruction animation
        gameObject.SetActive(false);
        // Optional: Notify the main boss script that a leg has been destroyed
        // boss.CheckLegsStatus();
    }
}