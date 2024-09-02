using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 1.0f;
    public float dragSpeed = 1.0f;

    private Camera mainCamera;
    private Vector3 dragOrigin;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Zoom with scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        mainCamera.fieldOfView += -scroll * zoomSpeed;
        mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, 10f, 80f); // Adjust min/max zoom here

        // Drag map with left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = mainCamera.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * -dragSpeed, 0, pos.y * -dragSpeed);

        transform.Translate(move, Space.World);
    }
}
