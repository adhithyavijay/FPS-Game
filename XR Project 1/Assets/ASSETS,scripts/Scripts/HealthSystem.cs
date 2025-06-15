using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // ✅ Added for Scene Loading

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text healthText;  // ✅ Assign from PlayerHealthInfoPanel
    [SerializeField] private Image healthBar;      // ✅ Assign from PlayerHealthInfoPanel

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateHealthUI();

        Debug.Log($"🟥 Player took {damage} damage! Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die(); // ✅ Now instantly calls Die()
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString(); // ✅ Updates health number
        }

        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth; // ✅ Updates UI bar
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("☠️ Player Died! Loading Game Over Scene...");

        SceneManager.LoadScene("GameOverScene"); // ✅ Instantly loads Game Over Scene
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        UpdateHealthUI();
    }
}
