using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Public variables to allow customization in the Unity Inspector
    public float moveDistanceX = 5f;  // Distance to move along the X axis
    public float moveDistanceY = 0f;  // Distance to move along the Y axis
    public float moveDistanceZ = 0f;  // Distance to move along the Z axis
    public float moveSpeed = 2f;      // Speed of the platform's movement

    private Vector3 startPosition;    // The initial position of the platform
    private Vector3 targetPosition;   // The target position the platform is moving towards
    private bool isMovingToTarget = true; // A flag to indicate the direction of movement

    // Start is called before the first frame update
    void Start()
    {
        // Record the platform's starting position when the game begins
        startPosition = transform.position;

        // Set the target position based on the input distances
        targetPosition = startPosition + new Vector3(moveDistanceX, moveDistanceY, moveDistanceZ);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        // Move the platform by interpolating between start and target position
        if (isMovingToTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
        }

        // Check if the platform has reached the target position, and reverse direction
        if (transform.position == targetPosition)
        {
            isMovingToTarget = false;
        }
        else if (transform.position == startPosition)
        {
            isMovingToTarget = true;
        }
    }
}
