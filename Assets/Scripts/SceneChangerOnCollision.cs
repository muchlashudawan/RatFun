using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerOnCollision : MonoBehaviour
{
    // Public field to set the target scene name in the Inspector
    public string sceneName;
    
    // Tag to identify the player
    public string playerTag = "Player";

    // This method is called when another collider enters the trigger collider attached to the GameObject this script is attached to
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider that entered the trigger has the player tag
        if (other.CompareTag(playerTag))
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneName);
        }
    }
}
