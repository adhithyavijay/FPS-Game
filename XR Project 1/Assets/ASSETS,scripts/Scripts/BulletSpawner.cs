using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;  // ✅ Assign Bullet Prefab in Inspector
    public Transform spawnPoint;     // ✅ Assign Gun Barrel (where bullets spawn)
    public float bulletSpeed = 50f;
    public float bulletLifetime = 3f;

    [SerializeField] private ParticleSystem muzzleFlash;  // ✅ Assign Muzzle Flash in Inspector
    [SerializeField] private AudioSource gunshotSound;    // ✅ Assign Gun Sound in Inspector

    public void SpawnBullet()
    {
        if (bulletPrefab == null || spawnPoint == null)
        {
            Debug.LogError("❌ Bullet Prefab or Spawn Point is NOT assigned!");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            bulletRb.linearVelocity = spawnPoint.forward * bulletSpeed;
        }

        // ✅ Play Muzzle Flash
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }
        else
        {
            Debug.LogError("❌ Muzzle Flash is NOT assigned in the Inspector!");
        }

        // ✅ Play Gunshot Sound
        if (gunshotSound != null)
        {
            gunshotSound.Play();
        }

        Destroy(bullet, bulletLifetime);
    }
}
