using UnityEngine;

public class BossLeg : MonoBehaviour
{
    public int legHealth = 50;
    private DamageEffect damageEffect;

    void Start()
    {
        damageEffect = GetComponent<DamageEffect>();
        if (damageEffect == null)
            Debug.LogError("DamageEffect script not found on the leg!");
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
    }
}