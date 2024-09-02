using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Store the current scene's name before transitioning
            SceneData.previousSceneName = SceneManager.GetActiveScene().name;
            // Load the Game Over scene
            SceneManager.LoadScene("Game Over");
        }
    }
}
