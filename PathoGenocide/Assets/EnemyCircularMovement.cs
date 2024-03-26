using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircularMovement : MonoBehaviour
{
    public float rotationSpeed = 50f; // Degrees per second
    public float circleRadius = 20f; // Distance from the center
    public Vector2 circleCenter = Vector2.zero; // Center of the circle

    private float currentAngle;

    private void Start()
    {
        // Initialize currentAngle based on current position
        Vector2 direction = transform.position - new Vector3(circleCenter.x, circleCenter.y, transform.position.z);
        currentAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    private void Update()
    {
        // Update the current angle based on rotation speed and time
        currentAngle += rotationSpeed * Time.deltaTime;

        // Ensure the angle wraps around 360 degrees
        currentAngle %= 360;

        // Calculate the new position
        float x = circleCenter.x + Mathf.Cos(currentAngle * Mathf.Deg2Rad) * circleRadius;
        float y = circleCenter.y + Mathf.Sin(currentAngle * Mathf.Deg2Rad) * circleRadius;

        // Update the enemy's position
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
