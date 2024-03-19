using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackHitbox;
    public float attackCooldown = 0.5f; // Time between attacks
    private bool isAttacking = false;
    private float cooldownTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isAttacking) // F key to attack
        {
            Debug.Log("F key pressed");
            StartCoroutine(PerformAttack());
        }

        if (isAttacking)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= attackCooldown)
            {
                isAttacking = false;
                cooldownTimer = 0;
            }
        }
    }

    private IEnumerator PerformAttack()
    {
        Debug.Log("Perform attack");
        isAttacking = true;
        attackHitbox.SetActive(true); // Activate the hitbox
        yield return new WaitForSeconds(0.1f); // Duration of the hitbox being active
        attackHitbox.SetActive(false); // Deactivate the hitbox
    }
}
