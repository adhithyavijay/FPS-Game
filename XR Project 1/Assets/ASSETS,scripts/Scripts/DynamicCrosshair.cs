using UnityEngine;

public class DynamicCrosshair : MonoBehaviour
{
    public RectTransform top, bottom, left, right;
    public float expandAmount = 20f;
    public float expandSpeed = 10f;
    private Vector2 originalPosition;

    void Start()
    {
        originalPosition = top.anchoredPosition;
    }

    void Update()
    {
        float expansion = 0;

        // Expand crosshair when moving
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            expansion = expandAmount;
        }

        // Expand crosshair when shooting
        if (Input.GetMouseButton(0)) // Left click for shooting
        {
            expansion = expandAmount * 1.5f;
        }

        // Apply smooth transition
        top.anchoredPosition = Vector2.Lerp(top.anchoredPosition, originalPosition + Vector2.up * expansion, Time.deltaTime * expandSpeed);
        bottom.anchoredPosition = Vector2.Lerp(bottom.anchoredPosition, originalPosition + Vector2.down * expansion, Time.deltaTime * expandSpeed);
        left.anchoredPosition = Vector2.Lerp(left.anchoredPosition, originalPosition + Vector2.left * expansion, Time.deltaTime * expandSpeed);
        right.anchoredPosition = Vector2.Lerp(right.anchoredPosition, originalPosition + Vector2.right * expansion, Time.deltaTime * expandSpeed);
    }
}
