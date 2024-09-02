using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowScore : MonoBehaviour
{
    public static ShowScore instance; // Singleton instance

    public GameObject spriteToShow; // Reference to the sprite GameObject (optional)
    public int scoreThreshold = 450; // Threshold value to show the sprite, configurable from the Inspector
    private Text scoreText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        scoreText = GetComponent<Text>();

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void FixedUpdate()
    {
        if (scoreText != null)
        {
            scoreText.text = Data.score.ToString("000");
        }

        // Check if spriteToShow is assigned and score is >= scoreThreshold to show the sprite
        if (spriteToShow != null)
        {
            bool shouldShowSprite = Data.score >= scoreThreshold;
            spriteToShow.SetActive(shouldShowSprite);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider's GameObject is tagged with "Player"
        if (other.CompareTag("Player"))
        {

            // Load the "Home" scene or any other scene based on your game logic
            SceneManager.LoadScene("Home");
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset Data.score based on scene (if needed)
        switch (scene.name)
        {
            case "Gameplay 2":
                Data.score = 450;
                break;
            case "Gameplay 3":
                Data.score = 795;
                break;
            default:
                Data.score = 0;
                break;
        }
    }
}
