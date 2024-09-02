using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private string enterSceneName = "SceneName"; // Default scene name for Enter key
    [SerializeField] private string escapeSceneName = "SceneName"; // Default scene name for Escape key
    [SerializeField] private bool escapeToQuit = false; // Checkbox to determine if Escape key quits the application

    private GameOver gameOverScript; // Reference to the GameOver script

    void Start()
    {
        // Find the GameOver script in the scene (assuming it's a persistent object)
        gameOverScript = FindObjectOfType<GameOver>();
    }

    void Update()
    {
        // Check if Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SwitchScene(enterSceneName);
        }

        // Check if Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escapeToQuit)
            {
                QuitApplication();
            }
            else
            {
                SwitchScene(escapeSceneName);
            }
        }
    }

    void SwitchScene(string sceneName)
    {
        // Check if the scene name is not empty or null
        if (!string.IsNullOrEmpty(sceneName))
        {
            // If switching to "Home" scene, reset the high score
            if (sceneName == "Home")
            {
                if (gameOverScript != null)
                {
                    gameOverScript.ResetHighScore();
                }
            }

            // Load the specified scene
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene name is empty or null.");
        }
    }

    void QuitApplication()
    {
        // Quit the application (only works in builds, not in the editor)
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
