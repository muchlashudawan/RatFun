using UnityEngine;

public class GrassMovement : MonoBehaviour
{
    // Variables to control the sway effect
    public float swaySpeed = 0.5f;  // Speed of the sway
    public float swayAmount = 0.1f;  // Amount of sway (vertical movement)
    public float swayOffset = 0.1f;  // Offset to make the sway unique

    // Initial position of the grass object
    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position of the grass object
        initialPosition = transform.position;

        // Give each grass object a unique offset based on its position
        swayOffset = Random.Range(0.0f, 100.0f);
    }

    void Update()
    {
        // Calculate the sway amount using Perlin noise
        float swayAmountY = Mathf.PerlinNoise(Time.time * swaySpeed, swayOffset) * swayAmount * 2.0f - swayAmount;

        // Apply the sway to the grass object's position
        transform.position = new Vector3(initialPosition.x, initialPosition.y + swayAmountY, initialPosition.z);
    }
}
