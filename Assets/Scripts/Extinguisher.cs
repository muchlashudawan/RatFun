using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public AudioClip destroySound; // The audio clip to play when the object is destroyed

    private bool hasCollided = false;  // Flag to track if collision has occurred
    private AudioSource audioSource;

    void Start()
    {
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
        if (collision.gameObject.CompareTag("Extinguisher") && !hasCollided)
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

            // Destroy the game object
            Destroy(gameObject, destroySound != null ? destroySound.length : 0f); // Destroy after sound plays if sound exists
        }
    }
}
