using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage = 10;
    public float bulletLifetime = 3f;
    private bool hasHit = false; // ✅ Prevents multiple hits

    void Start()
    {
        Destroy(gameObject, bulletLifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return; // ✅ Ensures bullet only applies damage once
        hasHit = true;

        Debug.Log($"⚠ Bullet hit: {collision.gameObject.tag}");

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(bulletDamage);
                Debug.Log($"✅ Bullet hit {collision.gameObject.name}! Dealing {bulletDamage} damage.");
            }

            Destroy(gameObject); // ✅ Bullet disappears when hitting an enemy
        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log($"🟫 Bullet hit a {collision.gameObject.tag} (ignored).");
            Destroy(gameObject); // ✅ Bullet disappears when hitting walls or ground
        }
        else
        {
            Debug.LogWarning($"❌ Bullet hit an object that is NOT an enemy! Object: {collision.gameObject.name}");
        }
    }
}
