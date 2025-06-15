using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake instance; // ✅ Singleton for easy access
    private Transform camTransform;
    private Vector3 originalPos;

    [SerializeField] private float shakeDuration = 0.1f;
    [SerializeField] private float shakeMagnitude = 0.1f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        camTransform = Camera.main.transform; // ✅ Automatically finds main camera
        if (camTransform != null)
        {
            originalPos = camTransform.localPosition;
        }
        else
        {
            Debug.LogError("❌ No Main Camera found! Assign the camera manually.");
        }
    }

    public void ShakeScreen()
    {
        if (camTransform != null)
        {
            StartCoroutine(Shake());
        }
        else
        {
            Debug.LogError("❌ Camera Transform is missing!");
        }
    }

    IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            camTransform.localPosition = originalPos + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;

            yield return null;
        }

        camTransform.localPosition = originalPos; // ✅ Reset position after shaking
    }
}
