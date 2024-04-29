using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public int maxHealth = 10;
    public bool followPlayer = true;
    public float moveSpeed = 5f;
    private int currentHealth;
    private Animator bodyAnimator;
    private Transform playerTransform;
    public GameObject dnaToken;
    [SerializeField] int tokenDropCount = 1;

    void Start()
    {
        currentHealth = maxHealth;
        bodyAnimator = GetComponentInChildren<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (followPlayer && playerTransform != null)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("PlayerAttack"))
        {
            Debug.Log("Collision with PlayerAttack");
            TakeDamage(10);
        }
        else if (collider.gameObject.CompareTag("Player"))
        {
            Player player = collider.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(10);
            }
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            DropDnaTokens();
            Die();
        }
    }
    void DropDnaTokens()
    {
        for (int i = 0; i < tokenDropCount; i++)
        {
            // Debug.Log("dropping token");
            GameObject token = DNATokenPoolController.Instance.GetDNAToken();
            token.transform.position = transform.position;
            token.transform.rotation = Quaternion.identity;
        }
    }
    void Die()
    {
        UIManager.Instance.UpdateKillCount();
        bodyAnimator.SetTrigger("Die");
        StartCoroutine(WaitAndReturnToPool(0.30f));
    }


    IEnumerator WaitAndReturnToPool(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        EnemyPoolController.Instance.ReturnEnemy(gameObject);
    }
}