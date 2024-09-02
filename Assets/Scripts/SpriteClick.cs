using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteClick : MonoBehaviour
{
    public string sceneName; // Public variable to input the scene name in the Unity Editor
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing from this GameObject");
            return;
        }
        
        originalColor = spriteRenderer.color;
    }

    void OnMouseDown()
    {
        // Change the color to a darker shade
        spriteRenderer.color = originalColor * 0.8f;
    }

    void OnMouseUp()
    {
        // Restore the original color
        spriteRenderer.color = originalColor;
        
        // Load the specified scene
        SceneManager.LoadScene(sceneName);
    }
}
