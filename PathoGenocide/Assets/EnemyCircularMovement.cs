using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircularMovement : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float circleRadius = 20f;
    public Vector2 circleCenter = Vector2.zero;

    private float currentAngle;

    private void Start()
    {
        Vector2 direction = transform.position - new Vector3(circleCenter.x, circleCenter.y, transform.position.z);
        currentAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    private void Update()
    {
        currentAngle += rotationSpeed * Time.deltaTime;
        currentAngle %= 360;

        float x = circleCenter.x + Mathf.Cos(currentAngle * Mathf.Deg2Rad) * circleRadius;
        float y = circleCenter.y + Mathf.Sin(currentAngle * Mathf.Deg2Rad) * circleRadius;

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
