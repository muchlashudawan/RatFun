using UnityEngine;

public class BarrelController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Destroy the barrel game object
            Destroy(gameObject);
        }
    }
}
