using UnityEngine;

public class Pot : MonoBehaviour
{
    public AudioClip destroySound; // The audio clip to play when the object is destroyed

    private bool hasCollided = false;  // Flag to track if collision has occurred
    private AudioSource audioSource;
    private Vector3 originalPosition; // Store the original position of the object

    void Start()
    {
        // Store the original position of the object
        originalPosition = transform.position;

        // Add an AudioSource component if one does not exist
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the destroy sound to the audio source
        if (destroySound != null)
        {
            audioSource.clip = destroySound;
            audioSource.playOnAwake = false;
        }
        else
        {
            Debug.LogWarning("No destroy sound assigned on GameObject: " + gameObject.name);
        }

        // Ensure the audio source is not spatial (no positional effect)
        audioSource.spatialBlend = 0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collided with this object
        if (collision.gameObject.CompareTag("Border") && !hasCollided)
        {
            Debug.Log("Player collided with: " + gameObject.name);

            // Set flag to true to indicate collision
            hasCollided = true;

            // Play the destroy sound at full volume
            if (destroySound != null)
            {
                audioSource.volume = 1f; // Set volume to maximum
                audioSource.PlayOneShot(destroySound);
            }

            // Reset the object to its original position
            ResetObject();
        }
    }

    void ResetObject()
    {
        // Move the object back to its original position
        transform.position = originalPosition;

        // Reset any other state or properties here if needed

        // Reset the collision flag
        hasCollided = false;
    }
}
