using UnityEngine;

public class EndPoint : MonoBehaviour
{
    // Configurable parameters for the floating effect
    public float amplitude = 0.2f;  // Height of the floating motion
    public float frequency = 10f;    // Speed of the floating motion

    // Starting position of the star
    private Vector3 startPos;

    void Start()
    {
        // Save the initial position of the star
        startPos = transform.position;
    }

    void Update()
    {
        // Calculate the new position
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;

        // Apply the new position to the star
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
