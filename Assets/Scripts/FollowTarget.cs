using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform player;
    public Transform Bg1;
    public Transform Bg2;
    public Transform Bg3;
    public Camera mainCamera; // Reference to the main camera
    
    public float zoomFactor = 1.0f; // Zoom factor for orthographic size or field of view

    void Start()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is not assigned!");
        }
        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned!");
        }
    }

    void Update()
    {
        if (player == null || mainCamera == null) return;

        if (player.position.x != transform.position.x && player.position.x > 0 && player.position.x < 12f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), 0.1f);
        }
        
        Bg1.transform.position = new Vector2(transform.position.x * 1.0f, Bg1.transform.position.y);
        Bg2.transform.position = new Vector2(transform.position.x * 0.8f, Bg2.transform.position.y);
        Bg3.transform.position = new Vector2(transform.position.x * 0.6f, Bg3.transform.position.y);

        // Determine if the camera is orthographic or perspective
        if (mainCamera.orthographic)
        {
            // Adjust camera zoom for orthographic camera
            float targetOrthographicSize = zoomFactor;
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetOrthographicSize, 0.1f);
            // Debug.Log("Orthographic camera zoom set to: " + mainCamera.orthographicSize);
        }
        else
        {
            // Adjust camera field of view for perspective camera
            float targetFieldOfView = zoomFactor;
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFieldOfView, 0.1f);
            // Debug.Log("Perspective camera FOV set to: " + mainCamera.fieldOfView);
        }

        // Adjust camera position
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, mainCamera.transform.position.z);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, 0.1f);
    }
}
