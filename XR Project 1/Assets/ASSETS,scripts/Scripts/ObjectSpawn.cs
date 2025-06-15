using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ObjectSpawn : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BulletSpawner bulletSpawner;
    [SerializeField] private AudioSource fireAudio;
    [SerializeField] private AudioSource reloadAudio;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private TextMeshProUGUI outOfAmmoText;
    [SerializeField] private TextMeshProUGUI fireCountText;
    [SerializeField] private TextMeshProUGUI maxAmmoText;
    [SerializeField] private Transform gunTransform;

    private Vector3 originalGunPosition;
    private bool isRecoiling = false;
    private int fireCount = 30;
    private int maxAmmo = 30;
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    void Start()
    {
        if (gunTransform != null)
        {
            originalGunPosition = gunTransform.localPosition;
        }

        // ✅ Hide "Out of Ammo" at the start
        if (outOfAmmoText != null)
        {
            outOfAmmoText.gameObject.SetActive(false);
        }

        ValidateAudioSources();
        UpdateUI();
    }

    void Update()
    {
        if (fireCount > 0 && Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }

        if (fireCount == 0 && Input.GetKeyDown(KeyCode.R))
        {
            ReloadBullets();
        }
    }

    private void Shoot()
    {
        if (bulletSpawner != null)
        {
            bulletSpawner.SpawnBullet();
        }

        fireCount--;

        // ✅ Ensure fire audio only plays if the AudioSource is assigned & enabled
        if (fireAudio != null && fireAudio.isActiveAndEnabled)
        {
            fireAudio.Play();
        }
        else
        {
            Debug.LogWarning("⚠ Fire Audio Source is missing or disabled! Check the ObjectSpawn Inspector.");
        }

        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        if (!isRecoiling)
        {
            StartCoroutine(RecoilGun());
        }

        if (ScreenShake.instance != null)
        {
            ScreenShake.instance.ShakeScreen();
        }
        else
        {
            Debug.LogWarning("⚠ ScreenShake instance is missing! Add 'ScreenShake.cs' to the Main Camera.");
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (fireCountText != null)
        {
            fireCountText.text = fireCount.ToString();
        }

        if (maxAmmoText != null)
        {
            maxAmmoText.text = maxAmmo.ToString();
        }

        if (outOfAmmoText != null)
        {
            outOfAmmoText.gameObject.SetActive(fireCount == 0);
        }
    }

    public void ReloadBullets()
    {
        fireCount = maxAmmo;

        if (reloadAudio != null && reloadAudio.isActiveAndEnabled)
        {
            reloadAudio.Play();
        }
        else
        {
            Debug.LogWarning("⚠ Reload Audio Source is missing or disabled! Check the ObjectSpawn Inspector.");
        }

        UpdateUI();
    }

    IEnumerator RecoilGun()
    {
        isRecoiling = true;
        if (gunTransform != null)
        {
            Vector3 recoilOffset = new Vector3(0, 0, -0.1f);
            gunTransform.localPosition += recoilOffset;
            yield return new WaitForSeconds(0.05f);
            gunTransform.localPosition = originalGunPosition;
        }
        isRecoiling = false;
    }

    private void ValidateAudioSources()
    {
        if (fireAudio == null)
        {
            Debug.LogError("❌ Fire Audio Source is NOT assigned in ObjectSpawn!");
        }
        if (reloadAudio == null)
        {
            Debug.LogError("❌ Reload Audio Source is NOT assigned in ObjectSpawn!");
        }
    }
}
