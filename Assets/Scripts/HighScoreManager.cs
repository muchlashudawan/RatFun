// using UnityEngine;

// public class HighScoreManager : MonoBehaviour
// {
//     private static int highscore;

//     // Initialize the high score from PlayerPrefs
//     void Start()
//     {
//         highscore = PlayerPrefs.GetInt("HS", 0);
//     }

//     // Reset the high score to 0 and save to PlayerPrefs
//     public static void ResetHighScore()
//     {
//         highscore = 0;
//         PlayerPrefs.SetInt("HS", highscore);
//         PlayerPrefs.Save();
//     }

//     // Get the current high score
//     public static int GetHighScore()
//     {
//         return highscore;
//     }

//     // Update the high score if the new score is higher
//     public static void UpdateHighScore(int newScore)
//     {
//         if (newScore > highscore)
//         {
//             highscore = newScore;
//             PlayerPrefs.SetInt("HS", highscore);
//             PlayerPrefs.Save();
//         }
//     }
// }
