using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyMovingPlatform : MonoBehaviour
{
    public float moveDistanceX = 5f;  // Distance to move along the X axis
    public float moveDistanceY = 0f;  // Distance to move along the Y axis
    public float moveDistanceZ = 0f;  // Distance to move along the Z axis
    public float moveSpeed = 2f;      // Speed of the platform's movement
    public float bounceHeight = 10f; // Height of the bounce when the player collides
    public float bounceFrequency = 1f; // Frequency of the platform's vertical bounce

    private Vector3 startPosition;    // The initial position of the platform
    private Vector3 targetPosition;   // The target position of the platform

    private void Start()
    {
        // Set the starting position and the target position of the platform
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(moveDistanceX, moveDistanceY, moveDistanceZ);
    }

    private void Update()
    {
        MovePlatformWithBounce();
    }

    private void MovePlatformWithBounce()
    {
        // Smooth movement between start and target position
        float t = Mathf.PingPong(Time.time * moveSpeed, 1f);
        Vector3 basePosition = Vector3.Lerp(startPosition, targetPosition, t);

        // Add a vertical bounce effect using sine wave
        basePosition.y += Mathf.Sin(Time.time * Mathf.PI * bounceFrequency) * 0.5f;

        // Update the platform's position
        transform.position = basePosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterMovement playerMovement = other.GetComponent<CharacterMovement>();
            if (playerMovement != null)
            {
                playerMovement.SetVerticalVelocity(Mathf.Sqrt(bounceHeight * -2f * playerMovement.normalGravity));
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Ensure the player doesn't get stuck or overridden
        if (collision.gameObject.CompareTag("Player"))
        {
            // Do not modify the player's position or override their horizontal movement
            // Ensure interaction is minimal
        }
    }
}
