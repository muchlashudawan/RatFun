using UnityEngine;
using System.Collections;

public class SignTrigger : MonoBehaviour
{
    public GameObject textImage; // Reference to the text image GameObject
    public Transform player; // Reference to the player transform
    public float fadeDuration = 1.0f; // Duration of the fade effect

    private SpriteRenderer textRenderer;
    private Color originalColor;
    private bool isFadingIn = false;
    private bool isFadingOut = false;

    private void Start()
    {
        textRenderer = textImage.GetComponent<SpriteRenderer>();
        if (textRenderer != null)
        {
            originalColor = textRenderer.color;
            originalColor.a = 0;
            textRenderer.color = originalColor; // Start with the text image hidden
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == player)
        {
            StartCoroutine(FadeIn());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == player)
        {
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeIn()
    {
        isFadingIn = true;
        isFadingOut = false;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            if (isFadingOut) yield break; // Stop if fading out starts
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(1);
        isFadingIn = false;
    }

    private IEnumerator FadeOut()
    {
        isFadingOut = true;
        isFadingIn = false;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            if (isFadingIn) yield break; // Stop if fading in starts
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(0);
        isFadingOut = false;
    }

    private void SetAlpha(float alpha)
    {
        if (textRenderer != null)
        {
            Color newColor = originalColor;
            newColor.a = alpha;
            textRenderer.color = newColor;
        }
    }
}
