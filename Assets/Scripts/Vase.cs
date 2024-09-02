using UnityEngine;
using System.Collections;

public class ChangeSpriteOnCollision : MonoBehaviour
{
    public Sprite newSprite; // The new sprite to change to on collision
    public AudioClip collisionSound; // The audio clip to play on collision
    public GameObject prefabToInstantiate; // The prefab to instantiate on top of the object
    public float transitionDuration = 1f; // Duration of the transition in seconds
    public float shakeIntensity = 0.1f; // Intensity of the shake
    public float shakeDuration = 0.5f; // Duration of the shake in seconds

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool hasCollided = false;  // Flag to track if collision has occurred
    private Vector3 startPosition; // Initial position of the object

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on GameObject: " + gameObject.name);
            return;
        }

        if (newSprite == null)
        {
            Debug.LogWarning("No new sprite assigned on GameObject: " + gameObject.name);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Add an AudioSource component if one does not exist
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (collisionSound == null)
        {
            Debug.LogWarning("No collision sound assigned on GameObject: " + gameObject.name);
        }

        startPosition = transform.position; // Store the initial position
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collided with this object
        if (collision.gameObject.CompareTag("Player") && !hasCollided)
        {
            Debug.Log("Player collided with: " + gameObject.name);

            // Set flag to true to indicate collision
            hasCollided = true;

            // Start coroutine to shake the object
            StartCoroutine(ShakeAndChangeSprite());
        }
    }

    IEnumerator ShakeAndChangeSprite()
    {
        // Shake the object
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            // Calculate a random shake position based on intensity
            Vector3 shakePosition = startPosition + Random.insideUnitSphere * shakeIntensity;

            // Apply the shake position
            transform.position = shakePosition;

            // Increment time
            elapsedTime += Time.deltaTime;

            // Wait until next frame
            yield return null;
        }

        // Reset the position after shaking is done
        transform.position = startPosition;

        // Change sprite to the new sprite
        spriteRenderer.sprite = newSprite;
        Debug.Log("Changed sprite to: " + newSprite.name);

        // Play collision sound
        if (collisionSound != null)
        {
            audioSource.PlayOneShot(collisionSound);
            Debug.Log("Played collision sound: " + collisionSound.name);
        }

        // Start coroutine to move prefab to top position
        StartCoroutine(MovePrefabToTopPosition());
    }

    IEnumerator MovePrefabToTopPosition()
    {
        // Calculate the target position (slightly above the current object)
        Vector3 targetPosition = transform.position + Vector3.up * 0.5f; // Adjust the '0.5f' to your desired height

        // Current position of the prefab (initial position)
        Vector3 initialPosition = transform.position;

        // Check if the prefabToInstantiate is still valid
        if (prefabToInstantiate == null)
        {
            yield break; // Exit the coroutine if the prefab is null
        }

        // Time elapsed during the transition
        float elapsedTime = 0f;

        // Loop until the transition duration is reached
        while (elapsedTime < transitionDuration)
        {
            // Check again if the prefabToInstantiate is null (in case it gets destroyed during the coroutine)
            if (prefabToInstantiate == null)
            {
                yield break; // Exit the coroutine if the prefab is null
            }

            // Calculate lerp value (0 to 1) based on elapsed time and duration
            float t = elapsedTime / transitionDuration;

            // Smoothly interpolate between initial position and target position
            prefabToInstantiate.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);

            // Increment time
            elapsedTime += Time.deltaTime;

            // Wait until next frame
            yield return null;
        }

        // Ensure the prefab ends up exactly at the target position
        if (prefabToInstantiate != null)
        {
            prefabToInstantiate.transform.position = targetPosition;
        }
    }
}
