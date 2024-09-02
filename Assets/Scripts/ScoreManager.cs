// using UnityEngine;

// public class ScoreManager : MonoBehaviour
// {
//     public static ScoreManager Instance { get; private set; }
//     public int Score { get; set; }

//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject); // Persist this object across scenes
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }
// }
