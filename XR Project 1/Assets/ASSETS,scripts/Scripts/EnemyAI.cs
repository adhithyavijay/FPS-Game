using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Movement & Targeting")]
    private NavMeshAgent agent;
    private Transform player;
    private HealthSystem playerHealth;

    [Header("UI Elements")]
    public Image healthBarFill;   // ✅ Assign in Inspector
    public GameObject healthBarCanvas; // ✅ Assign in Inspector

    [Header("Attack Settings")]
    public int attackDamage = 10; 
    public float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player")?.transform;

        if (player != null)
        {
            playerHealth = player.GetComponent<HealthSystem>();
        }

        if (agent == null)
        {
            Debug.LogError("❌ No NavMeshAgent found on enemy!");
            return;
        }

        ResetHealth();
    }

    private void Update()
    {
        if (player != null && agent.enabled)
        {
            if (!IsWallBetweenEnemyAndPlayer()) // ✅ Stops moving if a wall is in the way
            {
                agent.SetDestination(player.position);
            }
        }

        // ✅ Keep Health Bar above the enemy
        if (healthBarCanvas != null)
        {
            healthBarCanvas.transform.position = transform.position + new Vector3(0, 2.5f, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Time.time > lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log($"⚠ {gameObject.name} attacked Player! -{attackDamage} HP");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        Debug.Log($"{gameObject.name} took {damage} damage. Remaining HP: {currentHealth}");

        UpdateHealthUI();

        if (currentHealth == 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }

        if (healthBarCanvas != null)
        {
            healthBarCanvas.SetActive(currentHealth > 0);
        }
    }

    private void Die()
    {
        Debug.Log($"☠️ {gameObject.name} Died!");

        if (healthBarCanvas != null)
        {
            healthBarCanvas.SetActive(false);
        }

        agent.enabled = false;
        gameObject.SetActive(false);

        FindObjectOfType<EnemyManager>().RespawnEnemy(gameObject);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        if (healthBarCanvas != null)
        {
            healthBarCanvas.SetActive(true);
        }

        agent.enabled = true;
        gameObject.SetActive(true);
    }

    // ✅ NEW: Stops enemy from walking through walls
    private bool IsWallBetweenEnemyAndPlayer()
    {
        if (player == null) return false;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, distanceToPlayer))
        {
            if (hit.collider.CompareTag("Wall")) // ✅ Enemy stops moving if a wall is in the way
            {
                Debug.Log($"🚧 {gameObject.name} detected a wall and stopped moving!");
                return true;
            }
        }

        return false;
    }
}
