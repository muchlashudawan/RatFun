using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public string sceneName; // Public variable to input the scene name in the Unity Editor

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(LoadSceneOnClick);
    }

    void LoadSceneOnClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}
