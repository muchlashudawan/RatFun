using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI txScore;
    public TextMeshProUGUI txHighScore;

    private int currentScore; // Track the current score
    private int highscore; // Track the highest score

    void Start()
    {
        // Initialize current score from Data.score
        currentScore = Data.score;

        // Load highscore from PlayerPrefs (if available)
        highscore = PlayerPrefs.GetInt("HS", 0);

        // Display the current score
        txScore.text = "Score: " + currentScore;

        // Display the initial high score
        txHighScore.text = "Highscore: " + highscore;

        // Check and update high score if necessary based on the current score
        UpdateHighScore();
    }

    public void Replay()
    {
        // Reset game variables as needed
        Data.score = 0;
        EnemyController.EnemyKilled = 0;

        // Load the previous scene
        SceneManager.LoadScene(SceneData.previousSceneName);
    }

    public void UpdateHighScore()
    {
        // Update highscore only if current score is higher
        if (currentScore > highscore)
        {
            highscore = currentScore;
            txHighScore.text = "Highscore: " + highscore;

            // Save new highscore to PlayerPrefs
            PlayerPrefs.SetInt("HS", highscore);
            PlayerPrefs.Save();
        }
    }

    public void ResetHighScore()
    {
        // Reset highscore to 0
        highscore = 0;
        txHighScore.text = "Highscore: " + highscore;

        // Reset PlayerPrefs for highscore
        PlayerPrefs.SetInt("HS", highscore);
        PlayerPrefs.Save();
    }

    void OnEnable()
    {
        // If this script is enabled (i.e., when scene changes to GameOver), update high score
        UpdateHighScore();
    }

    void OnDisable()
    {
        // When this script is disabled (i.e., when scene changes away from GameOver), save PlayerPrefs
        PlayerPrefs.Save();
    }
}
