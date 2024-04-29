using System.Collections;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color damageColor = Color.white;
    public float colorChangeDuration = 0.1f;

    void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage()
    {
        //use virus leg logic
        StartCoroutine(FlashColor());
    }

    private IEnumerator FlashColor()
    {
        Debug.Log("flash color " + damageColor);
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = damageColor;  // Change color to red
        yield return new WaitForSeconds(colorChangeDuration);  // Wait for a few seconds
        spriteRenderer.color = originalColor;  // Revert to original color
    }
}